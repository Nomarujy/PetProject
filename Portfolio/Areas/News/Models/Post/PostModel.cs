using Portfolio.Models.Authorization;

namespace Portfolio.Areas.News.Models.Post
{
    public class PostModel
    {
        public PostModel() { }

        public PostModel(PostForm form, string AuthorEmail)
        {
            Title = form.Title;
            Content = form.Content;
            this.AuthorEmail = AuthorEmail;

            IsPubleched = form.IsPubleched;
            if (IsPubleched) EditedTime = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";

        public string AuthorEmail { get; set; } = null!;
        public User Author { get; set; } = null!;

        public bool IsPubleched { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public DateTime PubleshedTime { get; set; } = DateTime.UtcNow;
        public DateTime EditedTime { get; set; }
    }
}
