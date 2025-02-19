using FoodStore.BL.Helpers.DTOs.Auth;
using FoodStore.BL.Services.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers.Auth;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
    {
        await _userService.Register(registerDto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
    {
        var result = await _userService.Login(loginDto);
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        var result = await _userService.RefreshToken(refreshToken);
        return Ok(result);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgetPassword([FromForm] ForgetPasswordDto forgotPasswordDto)
    {
        return Ok(await _userService.ForgotPassword(forgotPasswordDto));
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto resetPasswordDto)
    {
        return Ok(await _userService.ResetPassword(resetPasswordDto));
    }

    [HttpPost("submit-register")]
    public async Task<IActionResult> SubmitRegistration([FromForm] SubmitRegistrationDto submitRegistrationDto)
    {
        return Ok(await _userService.SubmitRegistration(submitRegistrationDto));
    }

    [HttpPost("resend-confirmation-code")]
    public async Task<IActionResult> ResendConfirmationCode(string email)
    {
        var result = await _userService.ResendConfirmationCode(email);
        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _userService.Logout();
        return Ok("User logged out");
    }
}