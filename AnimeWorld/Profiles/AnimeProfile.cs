using AnimeWorld.Entities;
using AnimeWorld.Model.Anime;
using AutoMapper;

namespace AnimeWorld.Profiles
{
    public class AnimeProfile:Profile
    {
        public AnimeProfile()
        {
            //anime to animeDto
            CreateMap<Anime,AnimeDto>();
            //createAnimeDto to Anime
            CreateMap<CreateAnimeDto,Anime>();
            //anime to DetailedAnimeDto
            CreateMap<Anime, DetailedAnimeDto>().ForMember(dest => dest.Seasons, opt => opt.MapFrom(src => src.Seasons));
             

            //season to seasonDto (for detailedanimeDto)
            CreateMap<Season ,AnimeWorld.Model.Anime.SeasonDto>().ForMember(desc=>desc.Episodes,opt=>opt.MapFrom(src=>src.Episodes));

            //episode to episodeDto (for DetailedAnimeDto)
            //CreateMap<Episode, AnimeWorld.Model.Anime.EpisodeDto>();
        }
    }
}
 