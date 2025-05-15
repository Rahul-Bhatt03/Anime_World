using AnimeWorld.Entities;
using AnimeWorld.Model.User;
using AutoMapper;

namespace AnimeWorld.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile() {
            CreateMap<Users, UserDto>().ReverseMap(); // 👈 Adds both Users→UserDto and UserDto→Users
            CreateMap<RegisterUserDto, Users>();

        }
    }
}
