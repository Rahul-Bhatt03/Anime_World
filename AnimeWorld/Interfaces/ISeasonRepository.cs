using AnimeWorld.Entities;

namespace AnimeWorld.Interfaces
{
    public interface ISeasonRepository
    {
        Task<IEnumerable<Season>> GetSeasonsByAnimeIdAsync(int animeId);
        Task<Season>GetSeasonByIdAsync(int id);
        Task<Season>AddSeasonAsync(Season season);
        Task<Season> UpdateSeasonAsync(Season season);
        Task DeleteSeasonAsync(int id);
    }
}
