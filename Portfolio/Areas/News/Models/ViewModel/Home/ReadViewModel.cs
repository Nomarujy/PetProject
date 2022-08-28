using Portfolio.Areas.News.Models.Entity;

namespace Portfolio.Areas.News.Models.ViewModel.Home
{
    public class ReadViewModel
    {
        public Article Article { get; set; } = null!;
        public RecentlyViewModel Recently { get; set; } = null!;
    }
}
