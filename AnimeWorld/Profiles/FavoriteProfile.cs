using AnimeWorld.Entities;
using AnimeWorld.Model.History;
using AutoMapper;

namespace AnimeWorld.Profiles
{
    public class FavoriteProfile:Profile
    {
     public FavoriteProfile()
        {
            CreateMap<FavoriteAnime, FavoriteAnimeDto>()
           .ForMember(dest => dest.AnimeTitle, opt => opt.MapFrom(src => src.Anime.Title));
        }
    }
}
