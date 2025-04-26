using AnimeWorld.Entities;
using AnimeWorld.Model.Episode;
using AutoMapper;

namespace AnimeWorld.Profiles
{
    public class EpisodeProfile:Profile
    {
        public EpisodeProfile()
        {
            CreateMap<Episode, EpisodeDto>();
            CreateMap<CreateEpisodeDto, Episode>();
        }
    }
}
