// AnimeWorld.Services/FavoriteAnimeService.cs
using AnimeWorld.Data;
using AnimeWorld.Entities;
using AnimeWorld.Interfaces;
using AnimeWorld.Model.History;
using AnimeWorld.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class FavoriteAnimeService :IFavouriteAnimeService
{
    private readonly IFavouriteAnime _favoriteAnimeRepository;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public FavoriteAnimeService(
        IFavouriteAnime favoriteAnimeRepository,
        IMapper mapper,
        ApplicationDbContext context ,
    ICurrentUserService currentUserService
        ) 
    {
        _favoriteAnimeRepository = favoriteAnimeRepository;
        _mapper = mapper;
        _context = context;
        _currentUserService =currentUserService;
    }

    public async Task<IEnumerable<FavoriteAnimeDto>> AddFavouriteAnimeAsync(int userId, AddFavouriteAnime model)
    {
        Console.WriteLine($"Attempting to add favorite for UserId: {userId}, AnimeId: {model.AnimeId}");
          
        //removing user check since , it already exists in the icurentuserservice
        // Check if user exists
        //var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
        //if (!userExists)
        //    throw new KeyNotFoundException("User not found");

        // Check if anime exists
        var animeExists = await _context.Animes.AnyAsync(a => a.Id == model.AnimeId);
        if (!animeExists)
            throw new KeyNotFoundException("Anime not found");

        // Check if already favorited
        var alreadyFavorited = await _favoriteAnimeRepository.Exists(userId, model.AnimeId);
        if (alreadyFavorited)
            throw new InvalidOperationException("Anime is already in favorites");

        var favoriteAnime = new FavoriteAnime
        {
            UserId = userId,
            AnimeId = model.AnimeId,
        };

        await _favoriteAnimeRepository.AddFavoriteAnime(favoriteAnime);

        var result = await _favoriteAnimeRepository.GetFavoriteAnime(userId, model.AnimeId);
        return new List<FavoriteAnimeDto> { _mapper.Map<FavoriteAnimeDto>(result) };
    }

    public async Task<bool> RemoveFavouriteAnimeAsync(int userId, int animeId)
    {
        var favoriteAnime = await _favoriteAnimeRepository.GetFavoriteAnime(userId, animeId);
        if (favoriteAnime == null)
            return false;

        await _favoriteAnimeRepository.RemoveFavoriteAnime(favoriteAnime);
        return true;
    }

    public async Task<IEnumerable<FavoriteAnimeDto>> GetUserFavouriteAsync(int userId)
    {
        var favorites = await _favoriteAnimeRepository.GetUserFavoriteAnime(userId);
        return _mapper.Map<IEnumerable<FavoriteAnimeDto>>(favorites);
    }

    public async Task<bool> IsFavourite(int userId, int animeId)
    {
        return await _favoriteAnimeRepository.Exists(userId, animeId);
    }

}
