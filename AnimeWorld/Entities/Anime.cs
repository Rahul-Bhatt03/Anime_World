namespace AnimeWorld.Entities
{
    public class Anime
    {
        public Anime()
        {
            Seasons = new HashSet<Season>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Genre { get; set; }
        public string ThumbnailUrl { get; set; }

        public ICollection<Season> Seasons { get; set; }
    }
}
