using FoodStore.BL.Helpers.DTOs.Auth;

namespace FoodStore.BL.Services.Interfaces;

public interface IUserService
{
    Task Register(RegisterDto registerDto);

    Task<string> Login(LoginDto loginDto);

    Task<string> ForgotPassword(ForgetPasswordDto forgotPasswordDto);

    Task<string> ResetPassword(ResetPasswordDto resetPasswordDto);

    Task<string> SubmitRegistration(SubmitRegistrationDto registrationDto);
    
    Task<string> ResendConfirmationCode(string email);
    
    Task Logout();
}