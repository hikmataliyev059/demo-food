using FoodStore.BL.Helpers.DTOs.Coupon;
using FoodStore.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponController : ControllerBase
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    [HttpPost("apply")]
    public async Task<IActionResult> ApplyCoupon([FromBody] ApplyCouponRequestDto couponRequestDto)
    {
        var finalAmount = await _couponService.ApplyCouponAsync(couponRequestDto);
        return Ok(new { finalAmount });
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddCoupon([FromBody] CouponDto couponDto)
    {
        await _couponService.AddCouponAsync(couponDto);
        return Ok(new { message = "Coupon successfully added." });
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteCoupon([FromBody] string couponCode)
    {
        await _couponService.DeleteCouponAsync(couponCode);
        return Ok(new { message = "Coupon deleted successfully" });
    }

    [HttpPut("update/{couponCode}")]
    public async Task<IActionResult> UpdateCoupon(string couponCode, [FromBody] CouponUpdateDto couponDto)
    {
        await _couponService.UpdateCouponAsync(couponCode, couponDto);
        return Ok(new { message = "Coupon updated successfully" });
    }

    [HttpGet("get/{couponCode}")]
    public async Task<IActionResult> GetCoupon(string couponCode)
    {
        var coupon = await _couponService.GetCouponAsync(couponCode);
        return Ok(coupon);
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllCoupons()
    {
        return Ok(await _couponService.GetAllCouponsAsync());
    }
}