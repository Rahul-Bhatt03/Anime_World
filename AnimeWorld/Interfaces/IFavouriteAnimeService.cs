using AnimeWorld.Model.History;

namespace AnimeWorld.Interfaces
{
    public interface IFavouriteAnimeService
    {
        Task<IEnumerable<FavoriteAnimeDto>> AddFavouriteAnimeAsync(int userId,AddFavouriteAnime model);
        Task<bool> RemoveFavouriteAnimeAsync(int userId,int animeId);
        Task<IEnumerable<FavoriteAnimeDto>> GetUserFavouriteAsync(int userId);
        Task<bool> IsFavourite(int userId, int animeId);
    }
}
