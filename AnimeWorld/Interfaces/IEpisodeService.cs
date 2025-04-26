using AnimeWorld.Model.Episode;

namespace AnimeWorld.Interfaces
{
    public interface IEpisodeService
    {
        Task<EpisodeDto> GetEpisodeByIdAsync(int id);
        Task<IEnumerable<EpisodeDto>> GetEpisodesBySeasonIdAsync(int seasonId);
        Task<EpisodeDto> AddEpisodeAsync(CreateEpisodeDto createEpisodeDto);
        Task<EpisodeDto> UpdateEpisodeAsync(int id, CreateEpisodeDto updateEpisodeDto);
        Task DeleteEpisodeAsync(int id);
    }
}
