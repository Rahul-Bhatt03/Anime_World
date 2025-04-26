using AnimeWorld.Interfaces;
using AnimeWorld.Model.Anime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AnimeWorld.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimesController : ControllerBase
    {
        private readonly IAnimeService _animeService;
        private readonly ILogger<AnimesController> _logger;

        public AnimesController(
            IAnimeService animeService,
            ILogger<AnimesController> logger
            )
        {
            _animeService = animeService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimeDto>>>GetAllAnimes()
        {
            try
            {
                var animes = await _animeService.GetAllAnimesAsync();
                return Ok(animes);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Anime not found");
                return StatusCode(500, "internal server error");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimeDto>> GetAnimeById(int id)
        {
            try
            {
                var anime = await _animeService.GetAnimeByIdAsync(id);
                return Ok(anime);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Anime Not Found");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Error getting anime with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public async Task <ActionResult<DetailedAnimeDto>> CreateAnime([FromBody] CreateAnimeDto createAnimeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state: {@ModelState}", ModelState);
                    return BadRequest(ModelState);
                }
                _logger.LogInformation("Creating anime: {@Anime}", createAnimeDto);
                var anime = await _animeService.AddAnimeAsync(createAnimeDto);
                return CreatedAtAction(nameof(GetAnimeById), new { id = anime.Id }, anime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating anime. Request: {@Request}", createAnimeDto);
                return StatusCode(500,"Internal Server Error");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnime(int id ,[FromBody] CreateAnimeDto updateAnimeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _animeService.UpdateAnimeAsync(id, updateAnimeDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Anime not found for update");
                return NotFound(ex.Message);
            }
            catch (Exception ex) {
                _logger.LogError(ex, $"Error updating aanime with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnime(int id)
        {
            try
            {
                await _animeService.DeleteAnimeAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning($"KeyNotFound: {id}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error deleting anime with id {id}");
                return StatusCode(500, "internal server error");
            }
        }
    }
}
