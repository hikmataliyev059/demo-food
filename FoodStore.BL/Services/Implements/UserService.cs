using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Auth;
using FoodStore.BL.Helpers.Email;
using FoodStore.BL.Helpers.Exceptions.User;
using FoodStore.BL.Services.Interfaces;
using FoodStore.Core.Entities;
using FoodStore.Core.Entities.User;
using FoodStore.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FoodStore.BL.Services.Implements;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _configuration;

    public UserService(IMapper mapper, IMailService mailService, UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager, IConfiguration configuration)
    {
        _mapper = mapper;
        _mailService = mailService;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task Register(RegisterDto registerDto)
    {
        if (await _userManager.FindByEmailAsync(registerDto.Email) is not null)
        {
            throw new UserRegisterException();
        }

        AppUser user = _mapper.Map<AppUser>(registerDto);

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in result.Errors)
            {
                sb.Append(item.Description + " ");
            }

            throw new UserRegisterException(sb.ToString());
        }

        await _userManager.AddToRoleAsync(user, nameof(UserRoles.Member));

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmKey = new Random().Next(100000, 999999).ToString();
        user.ConfirmKey = confirmKey;
        await _userManager.UpdateAsync(user);
        var link = $"http://localhost:5029/submit-registration?email={registerDto.Email}&token={token}";

        MailRequest mailRequest = new MailRequest()
        {
            ToEmail = registerDto.Email,
            Subject = "Confirm Email",
            Body = $"<h1>Kodun: <br>{confirmKey}</h1><a href='{link}'>Confirm Email<a/>"
        };
        await _mailService.SendEmailAsync(mailRequest);
    }

    public async Task<string> SubmitRegistration(SubmitRegistrationDto registrationDto)
    {
        var user = await _userManager.FindByEmailAsync(registrationDto.Email);
        if (user == null) throw new EmailNotFoundException();

        if (string.IsNullOrEmpty(registrationDto.ConfirmKey) || string.IsNullOrEmpty(user.ConfirmKey))
        {
            throw new InvalidConfirmKeyException();
        }

        var currentTime = DateTime.UtcNow.AddHours(4);
        if (user.ConfirmKeyExpiration < currentTime)
        {
            await ResendConfirmationCode(user);
            throw new UserRegisterException("Your confirmation code has expired. A new code has been sent to your email");
        }

        if (registrationDto.ConfirmKey != user.ConfirmKey) throw new InvalidConfirmKeyException();

        user.EmailConfirmed = true;
        user.ConfirmKey = null;
        user.ConfirmKeyExpiration = null;

        await _userManager.UpdateAsync(user);

        return "Email verified!";
    }

    public async Task<string> ResendConfirmationCode(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) throw new EmailNotFoundException();

        await ResendConfirmationCode(user);

        return "A new confirmation code has been sent to your email.";
    }

    private async Task ResendConfirmationCode(AppUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmKey = new Random().Next(100000, 999999).ToString();
        user.ConfirmKey = confirmKey;
        user.ConfirmKeyExpiration = DateTime.UtcNow.AddHours(4).AddMinutes(3);
        await _userManager.UpdateAsync(user);

        var link = $"http://localhost:5029/submit-registration?email={user.Email}&token={token}";

        MailRequest mailRequest = new MailRequest()
        {
            ToEmail = user.Email,
            Subject = "Confirm Email",
            Body = $"<h1>Your new code: <br>{confirmKey}</h1><a href='{link}'>Confirm Email<a/>"
        };

        await _mailService.SendEmailAsync(mailRequest);
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.EmailOrUsername) ??
                   await _userManager.FindByNameAsync(loginDto.EmailOrUsername);

        if (user == null) throw new UserLoginFaildException();

        if (await _userManager.IsLockedOutAsync(user))
        {
            throw new UserLockedOutException();
        }

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!result)
        {
            await _userManager.AccessFailedAsync(user);
            throw new UserLoginFaildException();
        }

        await _userManager.ResetAccessFailedCountAsync(user);

        var _claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]!));

        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: _claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(4).AddMinutes(60)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return token;
    }

    public async Task<string> ForgotPassword(ForgetPasswordDto forgotPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
        if (user == null) throw new UserForgotException();

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var link = $"http://localhost:5029/reset-password?email={forgotPasswordDto.Email}&token={token}";

        MailRequest mailRequest = new MailRequest
        {
            ToEmail = forgotPasswordDto.Email,
            Subject = "Reset Password",
            Body = $"Click this link to reset your password: <a href='{link}'>Reset Password</a>",
        };
        await _mailService.SendEmailAsync(mailRequest);
        return "Password reset link has been sent to the provided email address, if it is registered";
    }

    public async Task<string> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user == null) throw new EmailNotFoundException();

        var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
        if (!result.Succeeded)
        {
            return "Your password has not been updated. Please try again!";
        }

        return "Your password has been successfully updated!";
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}