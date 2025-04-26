namespace AnimeWorld.Entities
{
    public class WatchHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Users Users { get; set; }

        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }

        public DateTime WatchedAt { get; set; } = DateTime.UtcNow;
    }
}
