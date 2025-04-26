using AnimeWorld.Interfaces;
using AnimeWorld.Model.Episode;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWorld.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodeService _episodeService;
        private readonly ILogger<EpisodesController> _logger;
        public EpisodesController(IEpisodeService episodeService, ILogger<EpisodesController> logger)
        {
            _episodeService = episodeService;
            _logger = logger;
        }

        [HttpGet("{id}")]
    public async Task<ActionResult<EpisodeDto>>GetEpisodeById
            (int id)
        {
            try
            {
                var episode = await _episodeService.GetEpisodeByIdAsync(id);
                return Ok(episode);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Episode not found");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting episode with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("season/{seasonId}")]
        public async Task<ActionResult<IEnumerable<EpisodeDto>>> GetEpisodeBySeasonId(int seasonId)
        {
            try
            {
                var episode = await _episodeService.GetEpisodesBySeasonIdAsync(seasonId);
                return Ok(episode);

            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Season not found for episodes");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting episodes for season {seasonId}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public async Task<ActionResult<EpisodeDto>> CreateEpisode([FromBody] CreateEpisodeDto createEpisodeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var episode = await _episodeService.AddEpisodeAsync(createEpisodeDto);
                return CreatedAtAction(nameof(GetEpisodeById), new { id = episode.Id }, episode);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Season not found for episode creation");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating episode");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEpisode(int id, [FromBody] CreateEpisodeDto updateEpisodeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _episodeService.UpdateEpisodeAsync(id, updateEpisodeDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Episode not found for update");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating episode with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpisode(int id)
        {
            try
            {
                await _episodeService.DeleteEpisodeAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Episode not found for deletion");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting episode with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
