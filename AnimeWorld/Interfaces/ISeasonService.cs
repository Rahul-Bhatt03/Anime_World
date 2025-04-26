using AnimeWorld.Model.Season;

namespace AnimeWorld.Interfaces
{
    public interface ISeasonService
    {
        Task<SeasonDto> GetSeasonByIdAsync(int id);
        Task<IEnumerable<SeasonDto>> GetSeasonsByAnimeIdAsync(int animeId);
        Task<SeasonDto> AddSeasonAsync(CreateSeasonDto createSeasonDto);
        Task<SeasonDto> UpdateSeasonAsync(int id, CreateSeasonDto updateSeasonDto);
        Task DeleteSeasonAsync(int id);
    }
}
