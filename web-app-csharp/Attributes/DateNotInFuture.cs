using System.ComponentModel.DataAnnotations;

namespace web_app_csharp.Attributes;

public class DateNotInFutureAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime dateValue)
        {
            if (dateValue > DateTime.Now)
            {
                return new ValidationResult(ErrorMessage ?? "Date cannot be in the future.");
            }
        }
        return ValidationResult.Success;
    }
}