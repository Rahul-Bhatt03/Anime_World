using System.ComponentModel.DataAnnotations;

namespace AnimeWorld.Model.History
{
    public class AddFavouriteAnime
    {
        [Required]
        public int AnimeId { get; set; }
    }
}
