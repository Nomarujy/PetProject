using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Extension
{
    public static class ValidationListExtension
    {
        public static void AddToResult(this List<ValidationResult> result, string message, string members)
        {
            result.Add(new ValidationResult(message, new string[1] { members }));
        }

        public static void AddToResult(this List<ValidationResult> result, string message, string[] members)
        {
            result.Add(new ValidationResult(message, members));
        }
    }
}
