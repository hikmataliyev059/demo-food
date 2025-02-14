using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Blogs;

namespace FoodStore.BL.Helpers.Validators.Blogs;

public class ArticleCreateDtoValidator : AbstractValidator<ArticleCreateDto>
{
    public ArticleCreateDtoValidator()
    {
        RuleFor(a => a.Title).NotEmpty().NotNull().WithMessage(ValidationMessages.Required)
            .MaximumLength(200).WithMessage(ValidationMessages.MaxLength);

        RuleFor(a => a.Content).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);

        RuleFor(a => a.CategoryId).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);

        RuleFor(a => a.TagIds).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);

        RuleFor(a => a.AuthorId).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);
    }
}