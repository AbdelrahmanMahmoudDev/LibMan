using System.ComponentModel.DataAnnotations;

namespace LibMan.Presentation.CustomValidationAttributes
{
    public class FullNameAttribute : ValidationAttribute
    {
        private ValidationResult? CheckStringIs4Words(string[] words)
        {
            if (words.Length != 4)
            {
                return new ValidationResult($"You must enter exactly your first 4 names. Current names: {words.Length}");
            }
            return null;
        }

        private ValidationResult? CheckWordIs2CharsAtleast(string[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length < 2)
                {
                    return new ValidationResult($"Each name must have atleast 2 characters, Invalid name: {words[i]}");
                }
            }
            return null;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is not string fullName || string.IsNullOrWhiteSpace(fullName))
            {
                return new ValidationResult("Full name cannot be empty.");
            }

            string[] words = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            ValidationResult? Result = CheckStringIs4Words(words);

            Result = CheckWordIs2CharsAtleast(words);

            return ValidationResult.Success;
        }
    }
}
