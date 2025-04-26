using AnimeWorld.Data;
using AnimeWorld.Entities;
using AnimeWorld.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimeWorld.Repositories
{
    public class EpisodeRepository:IEpisodeRepository
    {
        private readonly ApplicationDbContext _context;
        public EpisodeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Episode>>GetEpisodesBySeasonIdAsync(int seasonId)
        {
            return await _context.Episodes.Where(e=>e.SeasonId== seasonId).ToListAsync();
        }
        public async Task<Episode>GetEpisodeByIdAsync(int id)
        {
            return await _context.Episodes.FindAsync(id);
        }
        public async Task<Episode>AddEpisodeAsync(Episode episode)
        {
            await _context.Episodes.AddAsync(episode);
            await _context.SaveChangesAsync();
            return episode;
        }
        public async Task<Episode>UpdateEpisodeAsync(Episode episode)
        {
             _context.Episodes.Update(episode);
            await _context.SaveChangesAsync();
            return episode;
        }
        public async Task DeleteEpisodeAsync(int id)
        {
            var episode = await _context.Episodes.FindAsync(id);
            if (episode != null)
            {
                _context.Episodes.Remove(episode);
                await _context.SaveChangesAsync();
            }
            }
        }
}
