using Portfolio.Utilites.Extension;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Areas.News.Models.Post
{
    public class PostForm : IValidatableObject
    {
        public int Id { get; set; }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value.Replace("<", "&lt").Replace(">", "&gt");
            }
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value.Replace("<", "&lt").Replace(">", "&gt");
            }
        }

        public bool IsPubleched { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> res = new();

            if (Title == string.Empty) res.AddToResult("Заголовок не задан", "Title");

            return res;
        }
    }
}
