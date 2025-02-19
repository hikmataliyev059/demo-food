using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Product;
using FoodStore.BL.Helpers.Extensions.Slug;
using FoodStore.BL.Helpers.Validators.Common;

namespace FoodStore.BL.Helpers.Validators.Product;

public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RuleFor(product => product.Name).NotEmpty().NotNull().WithMessage(ValidationMessages.Required)
            .MinimumLength(3).WithMessage(ValidationMessages.MinLength);

        RuleFor(product => product.Slug).NotEmpty().NotNull().WithMessage(ValidationMessages.Required)
            .Matches(@"^[a-z0-9-]+$")
            .WithMessage("Slug must only contain lowercase letters, numbers, and hyphens")
            .MaximumLength(100).WithMessage(ValidationMessages.MaxLength)
            .Must((dto, slug) => string.IsNullOrEmpty(slug) || slug == SlugHelper.CreateSlug(dto.Name))
            .WithMessage("Slug must be generated from Name.");

        RuleFor(product => product.Price).NotEmpty().NotNull().WithMessage(ValidationMessages.Required)
            .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZero);

        RuleFor(product => product.Description).MaximumLength(1000).WithMessage(ValidationMessages.MaxLength);

        RuleFor(product => product.SKU).NotEmpty().NotNull().WithMessage(ValidationMessages.Required)
            .Matches(@"^[A-Za-z0-9\-]+$")
            .WithMessage("SKU can only contain letters, numbers, and dashes.");

        RuleFor(product => product.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ValidationMessages.GreaterThanZero);

        RuleFor(product => product.CategoryId).GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZero);

        RuleForEach(product => product.TagIds).GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZero);

        RuleFor(a => a.PrimaryImage).SetValidator(new ImageValidator());
        RuleForEach(a => a.Images).SetValidator(new ImageValidator());
    }
}