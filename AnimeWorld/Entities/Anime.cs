namespace AnimeWorld.Entities
{
    public class Anime
    {
        public Anime()
        {
            Seasons = new HashSet<Season>();
            CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow); // auto set the creation date
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Genre { get; set; }
        public string ThumbnailUrl { get; set; }
        public DateOnly CreatedAt { get; set; }

        public ICollection<Season> Seasons { get; set; }
    }
}
