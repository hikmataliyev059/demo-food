using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.File;

namespace FoodStore.BL.Helpers.Validators.File;

public class CreateUploadFileDtoValidator : AbstractValidator<CreateUploadFileDto>
{
    public CreateUploadFileDtoValidator()
    {
        RuleFor(x => x.File)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .Must(file => file == null || file.ContentType.StartsWith("image/"))
            .WithMessage(ValidationMessages.InvalidFileType)
            .Must(file => file == null || file.Length <= 3_000_000)
            .WithMessage(ValidationMessages.FileTooLarge);

        RuleFor(x => x.FolderName)
            .NotEmpty()
            .WithMessage(ValidationMessages.Required);
    }
}