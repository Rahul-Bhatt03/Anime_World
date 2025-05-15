using System.Text;
using AnimeWorld.Entities;
using AnimeWorld.Interfaces;
using AnimeWorld.Model.User;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AnimeWorld.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository,IPasswordHasher<Users> passwordHasher, IMapper mapper)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }
        public async Task<UserDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _repository.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return null;
            }
            //convert byte[] to string for verification
            string hashedPassword =Encoding.UTF8.GetString(user.PasswordHash);
            var result = _passwordHasher.VerifyHashedPassword(user,hashedPassword, loginDto.Password);
            return result == PasswordVerificationResult.Success ? _mapper.Map<UserDto>(user) : null;
        }
        public async Task<UserDto?> RegisterAsync(RegisterUserDto registerDto)
        {
            if (await _repository.FindByEmailAsync(registerDto.Email) != null)
                return null;

            var user = _mapper.Map<Users>(registerDto);
            string hashed = _passwordHasher.HashPassword(user, registerDto.Password);
            user.PasswordHash = Encoding.UTF8.GetBytes(hashed);
            user.Role = "User";
            await _repository.AddUserAsync(user);
            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto?>UpdateAsync(int id, UserDto updateUser)
        {
            var existingUser = await _repository.GetUserByIdAsync(id);
            if(existingUser==null)
            {
                return null;
            }
            var user=_mapper.Map<Users>(updateUser);
            await _repository.UpdateUserAsync(id,user);
            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto?> DeleteAsync(int id)
        {
            var user = await _repository.DeleteUserAsync(id);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<IEnumerable<UserDto?>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
        public async Task<UserDto?>GetUserById(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }
    }
}