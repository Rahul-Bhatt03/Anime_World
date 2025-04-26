using AnimeWorld.Entities;
using AnimeWorld.Interfaces;
using AnimeWorld.Model.Anime;
using AutoMapper;

namespace AnimeWorld.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IMapper _mapper;
        public AnimeService(IAnimeRepository animeRepository, IMapper mapper)
        {
            _animeRepository = animeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AnimeDto>> GetAllAnimesAsync()
        {
            var animes = await _animeRepository.GetAllAnimesAsync();
            return _mapper.Map<IEnumerable<AnimeDto>>(animes);
        }
        public async Task<DetailedAnimeDto> GetAnimeByIdAsync(int id)
        {
            var anime = await _animeRepository.GetAnimeByIdAsync(id);
            if (anime == null)
            {
                throw new KeyNotFoundException($"Anime with id{id}not found");
            }
            return _mapper.Map<DetailedAnimeDto>(anime);
        }
        public async Task<AnimeDto> AddAnimeAsync(CreateAnimeDto createAnimeDto)
        {
            var anime = _mapper.Map<Anime>(createAnimeDto);
            var addedAnime = await _animeRepository.AddAnimeAsync(anime);
            return _mapper.Map<AnimeDto>(addedAnime);
        }
        public async Task<AnimeDto> UpdateAnimeAsync(int id, CreateAnimeDto updateAnimeDto)
        {
            var existingAnime = await _animeRepository.GetAnimeByIdAsync(id);
            if (existingAnime == null)
            {
                throw new KeyNotFoundException($"Anime with id {id} not found");
            }
            _mapper.Map(updateAnimeDto, existingAnime);
            var updatedAnime = await _animeRepository.UpdateAnimeAsync(existingAnime);
            return _mapper.Map<AnimeDto>(updatedAnime);
        }
        public async Task DeleteAnimeAsync(int id)
        {
            await _animeRepository.DeleteAnimeAsync(id);
        }
    }
    }
