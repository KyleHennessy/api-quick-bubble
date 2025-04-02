
using System.ComponentModel.DataAnnotations;

namespace Application.Attributes
{
    public class ProfanityAttribute : ValidationAttribute
    {
        private readonly ProfanityFilter.ProfanityFilter _filter = new();

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var str = value as string;
            if(string.IsNullOrEmpty(str)){
                return ValidationResult.Success;
            }

            if (_filter.ContainsProfanity(str.ToLower()))
            {
                return new ValidationResult("Watch yo profanity");
            }

            return ValidationResult.Success;
        }
    }
}
