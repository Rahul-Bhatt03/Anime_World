using AnimeWorld.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimeWorld.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Anime> Animes { get; set; }

        public DbSet<Season> Seasons { get; set; }

        public DbSet<Episode>Episodes { get; set; }

        public DbSet<Users>Users { get; set; }

        public DbSet<FavoriteAnime> FavoriteAnimes { get; set; }

    }
}
