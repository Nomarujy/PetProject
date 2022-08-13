using System.ComponentModel.DataAnnotations;

namespace Portfolio.Areas.News.Models.FormModel
{
    public class CreateArticleForm
    {
        private string _title = string.Empty;
        private string _description = string.Empty;
        private string _content = string.Empty;

        private string Replace(string value) => value.Replace("<", "&lt").Replace(">", "&gt");

        [MaxLength(64)]
        public string Title
        {
            get { return _title; }
            set { _title = Replace(value); }
        }

        [MaxLength(128)]
        public string Description
        {
            get { return _description; }
            set { _description = Replace(value); }
        }
        public string Content
        {
            get { return _content; }
            set { _content = Replace(value); }
        }

        public bool IsPubleshed { get; set; } = false;
    }
}
