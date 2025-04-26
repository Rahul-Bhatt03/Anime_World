using AnimeWorld.Data;
using AnimeWorld.Entities;
using AnimeWorld.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimeWorld.Repositories
{
    public class SeasonRepository:ISeasonRepository
    {
        private readonly ApplicationDbContext _context;
        
        public SeasonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Season>>GetSeasonsByAnimeIdAsync(int animeId)
        {
            return await _context.Seasons.Include(s =>s.Episodes).Where(s =>s.AnimeId==animeId).ToListAsync();
        }
        public async Task<Season>GetSeasonByIdAsync(int id)
        {
            return await _context.Seasons.Include(s =>s.Episodes).FirstOrDefaultAsync(s =>s.Id==id);
        }
        public async Task<Season>AddSeasonAsync(Season season)
        {
            await _context.Seasons.AddAsync(season);
            await _context.SaveChangesAsync();
            return season;
        }
        public async Task<Season>UpdateSeasonAsync(Season season)
        {
            _context.Seasons.Update(season);
            await _context.SaveChangesAsync();
            return season;
        }
        public async Task DeleteSeasonAsync(int id)
        {
            var season = await _context.Seasons.FindAsync(id);
            if (season != null)
            {
                _context.Seasons.Remove(season);
                await _context.SaveChangesAsync();
            }
        }
    }
}
