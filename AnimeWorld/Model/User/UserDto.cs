using System.ComponentModel.DataAnnotations;

namespace AnimeWorld.Model.User
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Role { get; set; } = "User";
        public string Email { get; set; }
        public string? ProfileImageUrl { get; set; }

    }
}
