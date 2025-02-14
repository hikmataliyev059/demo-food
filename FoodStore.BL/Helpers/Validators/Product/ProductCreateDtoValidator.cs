using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Product;
using FoodStore.BL.Helpers.Extensions.Slug;

namespace FoodStore.BL.Helpers.Validators.Product;

public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Product name is required.")
            .MinimumLength(3)
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
            .NotEmpty()
            .NotNull()
            .WithMessage("Product price is required.")
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");

        RuleFor(product => product.Description)
            .MaximumLength(1000)
            .WithMessage("Description must be less than 1000 characters.");

        RuleFor(product => product.SKU)
            .NotEmpty()
            .NotNull()
            .WithMessage("SKU is required.")
            .Matches(@"^[A-Za-z0-9\-]+$")
            .WithMessage("SKU can only contain letters, numbers, and dashes.");

        RuleFor(product => product.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock quantity cannot be negative.");

        RuleFor(product => product.CategoryId)
            .GreaterThan(0)
            .WithMessage("CategoryId is required and must be valid.");

        RuleForEach(product => product.TagIds)
            .GreaterThan(0)
            .WithMessage("TagIds must be greater than 0.");

        RuleFor(product => product.PrimaryImageUrl)
            .Must(HasValidImageExtension)
            .WithMessage("Primary image must be a valid image file (e.g., jpg, png).");

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