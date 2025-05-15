using AnimeWorld.Model.User;

namespace AnimeWorld.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> LoginAsync(LoginDto loginDto);
        Task<UserDto?> RegisterAsync(RegisterUserDto registerDto);
        Task<UserDto?>UpdateAsync(int id,UserDto updateDto);
        Task<UserDto?> DeleteAsync(int id);
        Task<IEnumerable<UserDto?>> GetAllUsersAsync();
        Task<UserDto?> GetUserById(int id);
    }
}