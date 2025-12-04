using System.ComponentModel.DataAnnotations;
using DemoApp.DomainModel;
using DemoApp.Models;
using DemoApp.Services;

namespace DemoApp.Controllers.Attributes;

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
        
        if (emailIsTaken) return new ValidationResult(Errors.EmailIsTaken.Message);
        
        return ValidationResult.Success;
    }
}