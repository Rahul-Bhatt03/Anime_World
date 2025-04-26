using System.ComponentModel.DataAnnotations;

namespace AnimeWorld.Model.User
{
    public class RegisterUserDto
    {
   
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }

    }
}
