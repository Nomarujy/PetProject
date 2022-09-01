using Portfolio.Areas.News.Models.Entity;

namespace Portfolio.Areas.News.Models.ViewModel.Analytics
{
    public class AnalyticModel
    {
        public string AuthorId { get; set; } = null!;
        public int ArticleId { get; set; }
        public Article Article { get; set; } = null!;

        public int Views { get; set; }
        public int UnicalViews { get; set; }

        public int Today { get; set; }
        public int LastHour { get; set; }
    }
}
