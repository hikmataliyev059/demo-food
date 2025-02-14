using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Review;
using FoodStore.BL.Helpers.Email;
using FoodStore.BL.Helpers.Exceptions.Common;
using FoodStore.BL.Services.Interfaces;
using FoodStore.Core.Entities;
using FoodStore.Core.Entities.Products;
using FoodStore.Core.Entities.Reviews;
using FoodStore.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.BL.Services.Implements;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;
    private readonly IProductRepository _productRepository;

    public ReviewService(IReviewRepository reviewRepository, IMapper mapper, IMailService mailService,
        IProductRepository productRepository)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
        _mailService = mailService;
        _productRepository = productRepository;
    }

    public async Task AddReviewAsync(ReviewDto reviewDto)
    {
        var product = await _productRepository.GetByIdAsync(reviewDto.ProductId);

        if (product == null)
        {
            throw new NotFoundException<Product>();
        }

        var review = _mapper.Map<Review>(reviewDto);

        await _reviewRepository.AddAsync(review);
        await _reviewRepository.SaveChangesAsync();

        MailRequest adminMailRequest = new MailRequest
        {
            ToEmail = "marcelo850948@gmail.com",
            Subject = "Yeni Rəy Göndərildi",
            Body = $"Yeni bir rəy əlavə olundu:\n\n" + $"Məhsul ID: {reviewDto.ProductId}\n" +
                   $"Qiymətləndirmə: {reviewDto.Rating} ulduz\n" +
                   $"Şərh: {reviewDto.Comment}\n\n"
        };
        await _mailService.SendEmailAsync(adminMailRequest);

        var userSubject = new MailRequest
        {
            ToEmail = reviewDto.Email,
            Subject = "Təşəkkürlər! Rəyiniz üçün təşəkkür edirik",
            Body =
                "Rəyiniz üçün çox təşəkkür edirik. Komandamız rəylərinizi diqqətlə qiymətləndirir!\n\nƏn yaxşı arzularla,\nFoodStore Komandası"
        };

        await _mailService.SendEmailAsync(userSubject);
    }


    public async Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync(int productId)
    {
        var reviews = await _reviewRepository.GetReviewsByProductIdAsync(productId);

        var reviewList = reviews.ToList();

        if (reviewList.Count == 0) throw new NotFoundException<Product>();

        return reviewList.Select(r => _mapper.Map<ReviewDto>(r));
    }
}