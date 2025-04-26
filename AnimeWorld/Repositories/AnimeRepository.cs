using AnimeWorld.Data;
using AnimeWorld.Entities;
using AnimeWorld.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimeWorld.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly ApplicationDbContext _context;

        public AnimeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Anime>>GetAllAnimesAsync()
        {
            return await _context.Animes.Include(a => a.Seasons).ThenInclude(s => s.Episodes).ToListAsync();
        }
        public async Task<Anime> GetAnimeByIdAsync(int id)
        {
            return await _context.Animes.Include(a => a.Seasons).ThenInclude(s => s.Episodes).FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Anime>AddAnimeAsync(Anime anime)
        {
            await _context.Animes.AddAsync(anime);
            await _context.SaveChangesAsync();
            return anime;
        }
        public async Task<Anime>UpdateAnimeAsync(Anime anime)
        {
            _context.Animes.Update(anime);
            await _context.SaveChangesAsync();
            return anime;
        }
        public async Task DeleteAnimeAsync(int id)
        {
            var anime = await _context.Animes.FindAsync(id);
            if (anime != null)
            {
                _context.Animes.Remove(anime);
                await _context.SaveChangesAsync();
            }
        }
    }
}
