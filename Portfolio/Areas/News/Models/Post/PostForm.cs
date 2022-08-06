using Portfolio.Utilites.Extension;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Areas.News.Models.Post
{
    public class PostForm : IValidatableObject
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public bool IsPubleched { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> res = new();

            if (Title == string.Empty) res.AddToResult("Заголовок не задан", "Title");

            return res;
        }
    }
}
