using AnimeWorld.Data;
using AnimeWorld.Interfaces;
using AnimeWorld.Model.History;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteAnimeController : ControllerBase
    {
        private readonly IFavouriteAnimeService _favoriteAnimeService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ApplicationDbContext _context;

        public FavoriteAnimeController(
            IFavouriteAnimeService favoriteAnimeService,
            ICurrentUserService currentUserService,
            ApplicationDbContext context)
        {
            _favoriteAnimeService = favoriteAnimeService;
            _currentUserService = currentUserService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteAnimeDto>>> GetUserFavorites()
        {
            var userId = _currentUserService.UserId;
            var favorites = await _favoriteAnimeService.GetUserFavouriteAsync(userId);
            return Ok(favorites);
        }

        [HttpPost]
        public async Task<ActionResult<FavoriteAnimeDto>> AddFavoriteAnime(AddFavouriteAnime model)
        {
            await using var transation=await _context.Database.BeginTransactionAsync();
            //it is saying “Start a transaction. If anything goes wrong, roll back all the changes so the database stays clean and consistent.”
            try
            {
                var userId = _currentUserService.UserId;
                var result = await _favoriteAnimeService.AddFavouriteAnimeAsync(userId, model);
                return Ok(result.FirstOrDefault()); // Since the service returns IEnumerable
            }
            catch (UnauthorizedAccessException)
            {
                //transation is making sure that is anything wents wrong then it undo everything . 
                await transation.RollbackAsync();
                return Unauthorized();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{animeId}")]
        public async Task<IActionResult> RemoveFavoriteAnime(int animeId)
        {
            var userId = _currentUserService.UserId;
            var success = await _favoriteAnimeService.RemoveFavouriteAnimeAsync(userId, animeId);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("is-favorite/{animeId}")]
        public async Task<ActionResult<bool>> IsFavorite(int animeId)
        {
            var userId = _currentUserService.UserId;
            var isFavorite = await _favoriteAnimeService.IsFavourite(userId, animeId);
            return Ok(isFavorite);
        }
    }
}