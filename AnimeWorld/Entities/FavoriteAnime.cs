namespace AnimeWorld.Entities
{
    public class FavoriteAnime
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Users Users { get; set; }
        public int AnimeId { get; set; }  //stores fav anime id 
        public Anime Anime { get; set; }  //gets details about that anime like name,desc,title ,links etc whatever it has in its entity
    }
}
