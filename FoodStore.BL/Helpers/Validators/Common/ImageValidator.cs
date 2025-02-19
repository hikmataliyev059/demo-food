using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using Microsoft.AspNetCore.Http;

namespace FoodStore.BL.Helpers.Validators.Common;

public class ImageValidator : AbstractValidator<IFormFile>
{
    private const long MaxFileSize = 3 * 1024 * 1024;

    public ImageValidator()
    {
        RuleFor(file => file)
            .NotNull().WithMessage(ValidationMessages.Required)
            .Must(file => file.ContentType.StartsWith("image/"))
            .WithMessage(ValidationMessages.InvalidFileType)
            .Must(file => file.Length <= MaxFileSize)
            .WithMessage(ValidationMessages.FileTooLarge);
    }
}