namespace Portfolio.Areas.News.Models.ViewModel.Home
{
    public class DisplayArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public DateTime PubleshedTime { get; set; }

        public bool Published { get; set; }
    }
}
