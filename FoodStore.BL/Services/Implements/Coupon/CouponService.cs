using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Coupon;
using FoodStore.BL.Helpers.Exceptions.Coupon;
using FoodStore.BL.Services.Interfaces.Coupon;
using FoodStore.Core.Repositories.Interfaces.Coupons;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.BL.Services.Implements.Coupon;

public class CouponService : ICouponService
{
    private readonly ICouponRepository _couponRepository;
    private readonly IMapper _mapper;

    public CouponService(ICouponRepository couponRepository, IMapper mapper)
    {
        _couponRepository = couponRepository;
        _mapper = mapper;
    }

    public async Task<decimal> ApplyCouponAsync(ApplyCouponRequestDto couponRequestDto)
    {
        var coupon = await _couponRepository.GetCouponByCodeAsync(couponRequestDto.CouponCode);

        if (coupon == null) throw new CouponNotFoundException();

        if (coupon.ExpiryDate < DateTime.UtcNow || !coupon.IsActive)
            throw new CouponNotFoundException("Coupon can no longer be used");

        decimal discount;

        if (coupon.IsPercentage)
        {
            discount = couponRequestDto.TotalAmount * (coupon.DiscountAmount / 100);
        }
        else
        {
            discount = coupon.DiscountAmount;
        }

        var finalAmount = couponRequestDto.TotalAmount - discount;
        finalAmount = finalAmount < 0 ? 0 : finalAmount;

        return finalAmount;
    }

    public async Task AddCouponAsync(CouponDto couponDto)
    {
        var coupon = _mapper.Map<Core.Entities.Coupons.Coupon>(couponDto);

        await _couponRepository.AddAsync(coupon);
        await _couponRepository.SaveChangesAsync();
    }

    public async Task UpdateCouponAsync(string couponCode, CouponUpdateDto couponDto)
    {
        var existingCoupon = await _couponRepository.GetCouponByCodeAsync(couponCode);
        if (existingCoupon == null) throw new CouponNotFoundException();

        _mapper.Map(couponDto, existingCoupon);
        await _couponRepository.Update(existingCoupon);
        await _couponRepository.SaveChangesAsync();
    }

    public async Task<CouponDto?> GetCouponAsync(string couponCode)
    {
        var coupon = await _couponRepository.GetCouponByCodeAsync(couponCode);
        if (coupon == null) throw new CouponNotFoundException();

        var couponDto = _mapper.Map<CouponDto>(coupon);
        return couponDto;
    }

    public async Task<IEnumerable<CouponDto>> GetAllCouponsAsync()
    {
        var coupons = await _couponRepository.GetAll().ToListAsync();
        if (coupons == null) throw new CouponNotFoundException();
            
        var couponsDto = _mapper.Map<IEnumerable<CouponDto>>(coupons);
        return couponsDto;
    }

    public async Task DeleteCouponAsync(string couponCode)
    {
        var coupon = await _couponRepository.GetCouponByCodeAsync(couponCode);
        if (coupon == null) throw new CouponNotFoundException();

        await _couponRepository.Delete(coupon);
        await _couponRepository.SaveChangesAsync();
    }
}