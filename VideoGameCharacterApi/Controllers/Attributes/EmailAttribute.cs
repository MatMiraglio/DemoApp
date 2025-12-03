using System.ComponentModel.DataAnnotations;
using VideoGameCharacterApi.DomainModel;
using VideoGameCharacterApi.Models;
using VideoGameCharacterApi.Services;

namespace VideoGameCharacterApi.Controllers.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class EmailValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success; 

        string email = (value as string)!;

        Result<Email> emailResult = Email.TryCreate(email); 

        if (emailResult.IsFailure)
            return new ValidationResult(emailResult.ErrorMessage);
        
        var userService = validationContext.GetService(typeof(UserService)) as UserService;

        var emailIsTaken = userService.EmailExists(email);

        if (emailIsTaken) return new ValidationResult("Email is already taken");
        
        
        
        return ValidationResult.Success;
    }
}