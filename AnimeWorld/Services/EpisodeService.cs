using AnimeWorld.Entities;
using AnimeWorld.Interfaces;
//using AnimeWorld.Model.Anime;
using AnimeWorld.Model.Episode;
using AutoMapper;

namespace AnimeWorld.Services
{
    public class EpisodeService:IEpisodeService
    {
        private readonly IMapper _mapper;
        //private readonly IAnimeRepository animeRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly IEpisodeRepository _episodeRepository;
        public EpisodeService(IMapper mapper,ISeasonRepository seasonRepository,IEpisodeRepository episodeRepository)
        {
            _mapper = mapper;
            _seasonRepository = seasonRepository;
            _episodeRepository = episodeRepository;
        }
        public async Task<EpisodeDto> GetEpisodeByIdAsync(int id)
        {
            var Episode = await _episodeRepository.GetEpisodeByIdAsync(id);
            if (Episode == null)
            {
                throw new KeyNotFoundException($"episode with id {id}not found");

            }
            return _mapper.Map<EpisodeDto>(Episode);
        }
        public async Task<IEnumerable<EpisodeDto>>GetEpisodesBySeasonIdAsync(int seasonId)
        {
            var season = await _seasonRepository.GetSeasonByIdAsync(seasonId);
            if(season == null)
            {
                throw new KeyNotFoundException($"season with the id {seasonId} not found");
            }
            var episodes=await _episodeRepository.GetEpisodesBySeasonIdAsync(seasonId);
            return _mapper.Map<IEnumerable<EpisodeDto>>(episodes);
        }
        public async Task<EpisodeDto>AddEpisodeAsync(CreateEpisodeDto createEpisodeDto)
        {
            var season = await _seasonRepository.GetSeasonByIdAsync(createEpisodeDto.SeasonId);
            if (season == null)
            {
                throw new KeyNotFoundException($"Season with id{createEpisodeDto.SeasonId}not found");
            }
            var episode = _mapper.Map<Episode>(createEpisodeDto);
            var addedEpisode = await _episodeRepository.AddEpisodeAsync(episode);
            return _mapper.Map<EpisodeDto>(addedEpisode);
        }
        public async Task<EpisodeDto>UpdateEpisodeAsync(int id,CreateEpisodeDto updateEpisodeDto)
        {
            var existingEpisode=await _episodeRepository.GetEpisodeByIdAsync(id);
            if(existingEpisode == null)
            {
                throw new KeyNotFoundException($"episode with id {id} not found");
            }
            _mapper.Map(updateEpisodeDto, existingEpisode);
            var updatedEpisode = await _episodeRepository.UpdateEpisodeAsync(existingEpisode);
            return _mapper.Map<EpisodeDto>(updatedEpisode);
        }
        public async Task DeleteEpisodeAsync(int id)
        {
            await _episodeRepository.DeleteEpisodeAsync(id);
        }
    }
}
