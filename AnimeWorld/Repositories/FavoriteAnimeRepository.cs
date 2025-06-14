using System.Net.Mail;
using AnimeWorld.Data;
using AnimeWorld.Entities;
using AnimeWorld.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AnimeWorld.Repositories
{
    public class FavoriteAnimeRepository:IFavouriteAnime
    {
        private readonly ApplicationDbContext _context;
        public FavoriteAnimeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<FavoriteAnime> GetFavoriteAnime(int userId, int animeId)
        {
            return await _context.FavoriteAnimes
                .FirstOrDefaultAsync(fa => fa.UserId == userId && fa.AnimeId == animeId);
        }

        public async Task AddFavoriteAnime(FavoriteAnime anime)
        {
            //Attach the user to ensure relationship consistency that is if anime is not attached with the db then it attaches it with db .
            if (_context.Entry(anime).State == EntityState.Detached)
            {
                _context.Attach(anime); 
            }
            await _context.FavoriteAnimes.AddAsync(anime);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFavoriteAnime(FavoriteAnime favoriteAnime)
        {
            _context.FavoriteAnimes.Remove(favoriteAnime);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FavoriteAnime>>GetUserFavoriteAnime(int userId)
        {
            return await _context.FavoriteAnimes.Include(fa => fa.Anime).Where(fa => fa.UserId == userId).ToListAsync();
        }

        public async Task <bool>Exists(int userId,int animeId)
        {
            return await _context.FavoriteAnimes.AnyAsync(fa => fa.UserId == userId && fa.AnimeId == animeId);
        }

    }
}
