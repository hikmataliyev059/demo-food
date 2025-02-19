using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Product;
using FoodStore.BL.Helpers.Extensions.Slug;

namespace FoodStore.BL.Helpers.Validators.Product;

public class ProductGetDtoValidator : AbstractValidator<ProductGetDto>
{
    public ProductGetDtoValidator()
    {
        RuleFor(product => product.Id).GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZero);

        RuleFor(product => product.Name).NotEmpty().WithMessage(ValidationMessages.Required);

        RuleFor(product => product.Slug).NotEmpty().NotNull().WithMessage(ValidationMessages.Required)
            .Matches(@"^[a-z0-9-]+$").WithMessage("Slug must only contain lowercase letters, numbers, and hyphens")
            .MaximumLength(100).WithMessage(ValidationMessages.MaxLength)
            .Must((dto, slug) => string.IsNullOrEmpty(slug) || slug == SlugHelper.CreateSlug(dto.Name))
            .WithMessage("Slug must be generated from Name.");

        RuleFor(product => product.Price).GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZero);

        RuleFor(product => product.Description).MaximumLength(1000).WithMessage(ValidationMessages.MaxLength);

        RuleFor(product => product.SKU).NotEmpty().WithMessage(ValidationMessages.Required);

        RuleFor(product => product.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock quantity cannot be negative.");

        RuleFor(product => product.CategoryName).NotEmpty().WithMessage(ValidationMessages.Required);

        RuleForEach(product => product.TagIds).GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZero);
    }
}