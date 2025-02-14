using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Blogs;

namespace FoodStore.BL.Helpers.Validators.Blogs;

public class AuthorCreateDtoValidator : AbstractValidator<AuthorCreateDto>
{
    public AuthorCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);
        RuleFor(x => x.Bio).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);
        RuleFor(x => x.ImageUrl).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);
    }
}