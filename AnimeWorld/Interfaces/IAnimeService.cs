using AnimeWorld.Model.Anime;

namespace AnimeWorld.Interfaces
{
    public interface IAnimeService
    {
        Task<IEnumerable<AnimeDto>> GetAllAnimesAsync();
        Task<DetailedAnimeDto> GetAnimeByIdAsync(int id);
        Task<AnimeDto> AddAnimeAsync(CreateAnimeDto createAnimeDto);
        Task<AnimeDto> UpdateAnimeAsync(int id, CreateAnimeDto updateAnimeDto);
        Task DeleteAnimeAsync(int id);
    }
}
