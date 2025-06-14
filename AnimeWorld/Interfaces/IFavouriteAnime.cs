using AnimeWorld.Entities;

namespace AnimeWorld.Interfaces
{
    public interface IFavouriteAnime
    {
        Task<IEnumerable<FavoriteAnime>> GetUserFavoriteAnime(int userId);
        Task<FavoriteAnime> GetFavoriteAnime(int userId, int animeId);
        Task AddFavoriteAnime(FavoriteAnime favoriteAnime);
        Task RemoveFavoriteAnime(FavoriteAnime favoriteAnime);
        Task<bool> Exists(int userId, int animeId);
    }
}
