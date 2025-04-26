using AnimeWorld.Entities;

namespace AnimeWorld.Interfaces
{
    public interface IEpisodeRepository
    {
        Task<Episode> GetEpisodeByIdAsync(int id);
        Task<Episode> AddEpisodeAsync(Episode episode);
        Task<Episode> UpdateEpisodeAsync(Episode episode);
        Task DeleteEpisodeAsync(int id);
        Task<IEnumerable<Episode>> GetEpisodesBySeasonIdAsync(int seasonId);
    }
}
