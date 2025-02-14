namespace FoodStore.BL.Helpers.Constants;

public static class ValidationMessages
{
    public const string Required = "{PropertyName} cannot be empty or null";
    public const string MinLength = "Enter a minimum of {MinLength} characters";
    public const string MaxLength = "Enter a maximum of {MaxLength} characters";
    public const string InvalidEmail = "Enter the email type correctly";
    public const string PasswordCriteria = "Enter the correct password type";
    public const string PasswordMismatch = "Passwords don't match";
    public const string EmailOrUsernameRequired = "Email or Username cannot be empty or null";
    public const string PasswordRequired = "Password cannot be empty or null";
    public const string InvalidPhoneNumber = "Invalid phone number format";
    public const string GreaterThanZero = "{PropertyName} must be greater than 0";
    public const string InvalidFileType = "File must be in image format";
    public const string FileTooLarge = "File size cannot exceed 3MB";
}