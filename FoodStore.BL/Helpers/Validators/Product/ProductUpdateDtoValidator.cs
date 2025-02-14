using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Product;
using FoodStore.BL.Helpers.Extensions.Slug;

namespace FoodStore.BL.Helpers.Validators.Product;

public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateDtoValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .When(product => !string.IsNullOrEmpty(product.Name))
            .WithMessage("Product name cannot be empty if provided.")
            .MinimumLength(3)
            .When(product => !string.IsNullOrEmpty(product.Name))
            .WithMessage("Product name must be at least 3 characters long.");
        
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
            .When(product => product.Price.HasValue)
            .WithMessage("Price must be greater than 0 if provided.");

        RuleFor(product => product.Description)
            .MaximumLength(1000)
            .When(product => !string.IsNullOrEmpty(product.Description))
            .WithMessage("Description must be less than 1000 characters.");

        RuleFor(product => product.SKU)
            .Matches(@"^[A-Za-z0-9\-]+$")
            .When(product => !string.IsNullOrEmpty(product.SKU))
            .WithMessage("SKU can only contain letters, numbers, and dashes.");

        RuleFor(product => product.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .When(product => product.StockQuantity.HasValue)
            .WithMessage("Stock quantity cannot be negative.");

        RuleFor(product => product.CategoryId)
            .GreaterThan(0)
            .When(product => product.CategoryId.HasValue)
            .WithMessage("CategoryId must be valid if provided.");

        RuleForEach(product => product.TagIds)
            .GreaterThan(0)
            .When(product => product.TagIds.Any())
            .WithMessage("TagIds must be greater than 0.");

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