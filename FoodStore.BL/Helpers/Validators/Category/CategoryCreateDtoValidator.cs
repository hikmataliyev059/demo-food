using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Category;
using FoodStore.BL.Helpers.Extensions.Slug;

namespace FoodStore.BL.Helpers.Validators.Category;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .MinimumLength(3)
            .WithMessage(ValidationMessages.MinLength)
            .MaximumLength(50)
            .WithMessage(ValidationMessages.MaxLength);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .Matches(@"^[a-z0-9-]+$")
            .WithMessage("Slug must only contain lowercase letters, numbers, and hyphens")
            .MaximumLength(100)
            .WithMessage(ValidationMessages.MaxLength)
            .Must((dto, slug) => string.IsNullOrEmpty(slug) || slug == SlugHelper.CreateSlug(dto.Name))
            .WithMessage("Slug must be generated from Name.");
    }
}