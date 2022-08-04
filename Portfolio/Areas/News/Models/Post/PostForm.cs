using System.Drawing;
using System.ComponentModel.DataAnnotations;
using Portfolio.Utilites.Extension;

namespace Portfolio.Areas.News.Models.Post
{
    public class PostForm : IValidatableObject
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Image? MainImage { get; set; } = null;

        public bool isPubleched { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> res = new();

            if (Title == string.Empty) res.AddToResult("Заголовок не задан", "Title");

            return res;
        }
    }
}
