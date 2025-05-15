namespace AnimeWorld.Model.Anime
{
    //for fetching animes, what does frontend needs 
    public class AnimeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Genre { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
