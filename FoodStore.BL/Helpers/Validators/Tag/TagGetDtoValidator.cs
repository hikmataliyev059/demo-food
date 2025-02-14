using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Tag;
using FoodStore.BL.Helpers.Extensions.Slug;

namespace FoodStore.BL.Helpers.Validators.Tag;

public class TagGetDtoValidator : AbstractValidator<TagGetDto>
{
    public TagGetDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Tag name cannot be empty")
            .MinimumLength(3)
            .WithMessage("Tag name must be at least 3 characters long")
            .MaximumLength(50)
            .WithMessage("Tag name must be between 3 and 50 characters");

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