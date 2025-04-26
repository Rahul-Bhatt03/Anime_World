namespace AnimeWorld.Model.Episode
{
    public class EpisodeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int EpisodeNumber { get; set; }
        public string VideoUrl { get; set; }
        public int SeasonId { get; set; }
    }
}
