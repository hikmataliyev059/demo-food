using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Blogs;

namespace FoodStore.BL.Helpers.Validators.Blogs;

public class CommentGetDtoValidator : AbstractValidator<CommentGetDto>
{
    public CommentGetDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZero);
    }
}