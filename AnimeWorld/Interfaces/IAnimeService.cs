using AnimeWorld.Model.Anime;

public interface IAnimeService
{
    Task<IEnumerable<AnimeDto>> GetAllAnimesAsync();
    Task<DetailedAnimeDto> GetAnimeByIdAsync(int id);
    Task<DetailedAnimeDto> AddAnimeAsync(CreateAnimeDto createAnimeDto); // Changed
    Task<DetailedAnimeDto> UpdateAnimeAsync(int id, CreateAnimeDto updateAnimeDto); // Changed
    Task<bool> DeleteAnimeAsync(int id); // Changed

}
