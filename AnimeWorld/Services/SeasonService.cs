using AnimeWorld.Entities;
using AnimeWorld.Interfaces;
using AnimeWorld.Model.Season;
using AutoMapper;

namespace AnimeWorld.Services
{
    public class SeasonService:ISeasonService
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly IAnimeRepository _animeRepository;
        private readonly IMapper _mapper;
        public SeasonService(ISeasonRepository seasonRepository,IAnimeRepository animeRepository,IMapper mapper)
        {
            _seasonRepository = seasonRepository;
            _animeRepository = animeRepository;
            _mapper = mapper;
        }
        public async Task<SeasonDto> GetSeasonByIdAsync(int id)
        {
            var season = await _seasonRepository.GetSeasonByIdAsync(id);
            if (season == null)
            {
                throw new KeyNotFoundException($"Season with id {id}not found");
            }
            return _mapper.Map<SeasonDto>(season);
        }
        public async Task<IEnumerable<SeasonDto>>GetSeasonsByAnimeIdAsync(int animeID)
        {
            var anime=await _animeRepository.GetAnimeByIdAsync(animeID);
            if(anime == null)
            {
                throw new KeyNotFoundException($"anime with id {animeID}not found");
            }
            var seasons=await _seasonRepository.GetSeasonsByAnimeIdAsync(animeID);
            return _mapper.Map<IEnumerable<SeasonDto>>(seasons);
        }
        public async Task<SeasonDto>AddSeasonAsync(CreateSeasonDto createSeasonDto)
        {
            var anime = await _animeRepository.GetAnimeByIdAsync(createSeasonDto.AnimeId);
            if (anime == null)
            {
                throw new KeyNotFoundException($"Anime with id {createSeasonDto.AnimeId} not found ");
            }
            var season = _mapper.Map<Season>(createSeasonDto);
            var addedSeason = await _seasonRepository.AddSeasonAsync(season);
            return _mapper.Map<SeasonDto>(addedSeason);
        }
        public async Task<SeasonDto>UpdateSeasonAsync(int id,CreateSeasonDto updateSeasonDto)
        {
            var existingSeason=await _seasonRepository.GetSeasonByIdAsync(id);
            if(existingSeason == null)
            {
                throw new KeyNotFoundException($"season with id {id} not found");
            }
            _mapper.Map(updateSeasonDto, existingSeason);
            var updatedSeason = await _seasonRepository.UpdateSeasonAsync(existingSeason);
            return _mapper.Map<SeasonDto>(updatedSeason);
        }
        public async Task DeleteSeasonAsync(int id)
        {
            await _seasonRepository.DeleteSeasonAsync(id);
        }
    }
}
