using FoodStore.BL.Helpers.DTOs.Auth;

namespace FoodStore.BL.Services.Interfaces.Auth;

public interface IUserService
{
    Task Register(RegisterDto registerDto);

    Task<TokenDto> Login(LoginDto loginDto);
    
    Task<TokenDto> RefreshToken(string refreshToken);

    Task<string> ForgotPassword(ForgetPasswordDto forgotPasswordDto);

    Task<string> ResetPassword(ResetPasswordDto resetPasswordDto);

    Task<string> SubmitRegistration(SubmitRegistrationDto registrationDto);
    
    Task<string> ResendConfirmationCode(string email);
    
    Task Logout();
}