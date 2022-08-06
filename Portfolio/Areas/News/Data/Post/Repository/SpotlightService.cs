namespace Portfolio.Areas.News.Data.Post.Repository
{
    public class SpotlightService : ISpotlightService
    {
        private readonly List<Requested> requested = new();

        public SpotlightService()
        {
            requested.Add(new Requested() { Id = 1, Count = 0 });
        }

        // Mock Database

        public int CurrentSpotlight => requested.First(c => c.Count == requested.Max(c => c.Count)).Id;
        public void PostRequested(int Id)
        {
            int Index = requested.FindIndex(c => c.Id == Id);

            if (Index == -1)
            {
                requested.Add(new Requested() { Id = Id, Count = 1 });
            }
            else
            {
                requested[Index].Count++;
            }
        }
    }


    public class Requested
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
