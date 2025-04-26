using AnimeWorld.Model.Episode;
using AnimeWorld.Model.Season;

namespace AnimeWorld.Model.Anime
{
    public class DetailedAnimeDto
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public List<SeasonDto> Seasons{ get; set; }
    }

    public class SeasonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EpisodeDto> Episodes { get; set; }
    }
    //public class EpisodeDto
    //{
    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //    public int EpisodeNumber { get; set; }
    //    public string VideoUrl { get; set; }
    //}
    }
