namespace Portfolio.Areas.News.Data.Post.Repository
{
    public interface ISpotlightService
    {
        public void PostRequested(int Id);
        public int CurrentSpotlight { get; }
    }
}
