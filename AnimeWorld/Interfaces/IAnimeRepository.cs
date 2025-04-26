using AnimeWorld.Entities;

namespace AnimeWorld.Interfaces
{
    public interface IAnimeRepository
    {
        Task<IEnumerable<Anime>> GetAllAnimesAsync();
        Task<Anime> GetAnimeByIdAsync(int id);
        Task<Anime> AddAnimeAsync(Anime anime);
        Task<Anime> UpdateAnimeAsync(Anime anime);
        Task DeleteAnimeAsync(int id);


    }
}
