namespace AnimeWorld.Model.Episode
{
    public class CreateEpisodeDto
    {
        public string Title { get; set; }
        public int EpisodeNumber { get; set; }
        public string VideoUrl { get; set; }
        public int SeasonId { get; set; }
    }
}
