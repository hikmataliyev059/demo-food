using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Product;
using FoodStore.BL.Helpers.Extensions.Slug;

namespace FoodStore.BL.Helpers.Validators.Product;

public class ProductGetDtoValidator : AbstractValidator<ProductGetDto>
{
    public ProductGetDtoValidator()
    {
        RuleFor(product => product.Id)
            .GreaterThan(0)
            .WithMessage("Product ID must be a valid number.");

        RuleFor(product => product.Name)
            .NotEmpty()
            .WithMessage("Product name is required.");

        RuleFor(product => product.Slug)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .Matches(@"^[a-z0-9-]+$")
            .WithMessage("Slug must only contain lowercase letters, numbers, and hyphens")
            .MaximumLength(100)
            .WithMessage(ValidationMessages.MaxLength)
            .Must((dto, slug) => string.IsNullOrEmpty(slug) || slug == SlugHelper.CreateSlug(dto.Name))
            .WithMessage("Slug must be generated from Name.");

        RuleFor(product => product.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");

        RuleFor(product => product.Description)
            .MaximumLength(1000)
            .WithMessage("Description must be less than 1000 characters.");

        RuleFor(product => product.SKU)
            .NotEmpty()
            .WithMessage("SKU is required.");

        RuleFor(product => product.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock quantity cannot be negative.");

        RuleFor(product => product.CategoryName)
            .NotEmpty()
            .WithMessage("Category name is required.");

        RuleForEach(product => product.TagIds)
            .GreaterThan(0)
            .WithMessage("TagIds  must be greater than 0.");

        RuleForEach(product => product.SubImageUrls)
            .Must(HasValidImageExtension)
            .WithMessage("Each image must be a valid image file (e.g., jpg, png).");
    }

    private static bool HasValidImageExtension(string filePath)
    {
        var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        var fileExtension = Path.GetExtension(filePath).ToLower();

        return validExtensions.Contains(fileExtension);
    }
}