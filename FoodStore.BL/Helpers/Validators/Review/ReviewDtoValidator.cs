using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Review;

namespace FoodStore.BL.Helpers.Validators.Review;

public class ReviewDtoValidator : AbstractValidator<ReviewDto>
{
    public ReviewDtoValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be between 1 and 5.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .MinimumLength(3).WithMessage(ValidationMessages.MinLength)
            .EmailAddress().WithMessage(ValidationMessages.InvalidEmail);

        RuleFor(x => x.Comment)
            .MaximumLength(1000).WithMessage(ValidationMessages.MaxLength);

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .GreaterThan(0).WithMessage(ValidationMessages.MinLength);
    }
}