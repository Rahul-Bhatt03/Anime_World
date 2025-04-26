using AnimeWorld.Entities;
using AnimeWorld.Model.Anime;
using AnimeWorld.Model.Season;
using AutoMapper;

namespace AnimeWorld.Profiles
{
    public class SeasonProfile : Profile
    {
        public SeasonProfile()
        {
            CreateMap<Season, AnimeWorld.Model.Season.SeasonDto>();
            CreateMap<CreateSeasonDto, Season>();
        }
    }
}