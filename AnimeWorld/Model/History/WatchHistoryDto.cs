namespace AnimeWorld.Model.History
{
    public class WatchHistoryDto
    {
        public int Id { get; set; }
        public int EpisodeId { get; set; }
        public DateTime WatchedAt { get; set; }
    }
}
