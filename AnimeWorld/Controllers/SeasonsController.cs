using AnimeWorld.Interfaces;
using AnimeWorld.Model.Season;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWorld.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SeasonsController : ControllerBase
    {
        private readonly ISeasonService _seasonService;
        private readonly ILogger<SeasonsController> _logger;

        public SeasonsController(ISeasonService seasonService, ILogger<SeasonsController> logger)
        {
            _seasonService = seasonService;
            _logger = logger;
        }
        [HttpGet("anime/{animeId}")]
        public async Task<ActionResult<IEnumerable<SeasonDto>>> GetSeasonsByAnimeId(int animeId)
        {
            try
            {
                var seasons = await _seasonService.GetSeasonsByAnimeIdAsync(animeId);
                return Ok(seasons);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "anime not found for seasons");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"error getting seasons for anim {animeId}");
                return StatusCode(500, "internal server error");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SeasonDto>> GetSeasonById(int id)
        {
            try
            {
                var season = await _seasonService.GetSeasonByIdAsync(id);
                return Ok(season);

            }
            catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "season with id {id} not found");
                return NotFound(ex.Message);
            } catch (Exception ex)
            {
                _logger.LogError(ex, $"error getting season with id {id}");
                return StatusCode(500, "internal server error");
            }
        }
        [HttpPost]
        public async Task<ActionResult<SeasonDto>> CreateSeason([FromBody] CreateSeasonDto createSeasonDto)
        {
            try
            {
                var season = await _seasonService.AddSeasonAsync(createSeasonDto);
                return Ok(season);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "anime not found for season creation");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating season");
                return StatusCode(500, "internal server error");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateSeason(int id,[FromBody]CreateSeasonDto updateSeasonDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _seasonService.UpdateSeasonAsync(id, updateSeasonDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Season not found for update");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating season with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteSeason(int id)
        {
            try
            {
                await _seasonService.DeleteSeasonAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Season not found for deletion");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting season with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
