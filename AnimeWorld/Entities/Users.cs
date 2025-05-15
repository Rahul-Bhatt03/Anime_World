using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System;
using System.ComponentModel.DataAnnotations;

namespace AnimeWorld.Entities
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string Role { get; set; } = "User";
        public string Email { get; set; }
        public string? ProfileImageUrl { get; set; }
        public bool IsActive { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        //navigation
        public ICollection<WatchHistory> WatchHistory { get; set; }
        public ICollection<FavoriteAnime> FavoriteAnime { get; set; }
    }
}
