using AnimeWorld.Entities;

namespace AnimeWorld.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users?> GetUserByIdAsync(int id);
        Task<Users?> FindByEmailAsync(string email);
        Task<Users> AddUserAsync(Users user);
        Task<Users> UpdateUserAsync(int id,Users user);
        Task<Users> DeleteUserAsync(int id);
    }
}
