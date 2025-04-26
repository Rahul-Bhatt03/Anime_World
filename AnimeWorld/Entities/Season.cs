namespace AnimeWorld.Entities
{
    public class Season
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AnimeId { get; set; }  //establish relation betn season and anime
        public Anime Anime { get; set; }

        public ICollection<Episode> Episodes { get; set; }
    }
}
