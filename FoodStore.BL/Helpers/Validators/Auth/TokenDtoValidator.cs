using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Auth;

namespace FoodStore.BL.Helpers.Validators.Auth;

public class TokenDtoValidator : AbstractValidator<TokenDto>
{
    public TokenDtoValidator()
    {
        RuleFor(x => x.AccessToken).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);
        RuleFor(x => x.RefreshToken).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);
    }
}