using AnimeWorld.Data;
using AnimeWorld.Entities;
using AnimeWorld.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimeWorld.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Users?> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Users> AddUserAsync(Users user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("DbUpdateException: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                throw; // Rethrow to keep original error if needed
            }
        }


        public async Task<Users?> UpdateUserAsync(int id, Users updatedUser)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return null;

            // Map onto the existing entity (avoids changing the Id)
            //_context.Entry(existingUser).CurrentValues.SetValues(updatedUser);
            existingUser.Name = updatedUser.Username;  
            existingUser.Email = updatedUser.Email;
            existingUser.Role = updatedUser.Role;
            existingUser.ProfileImageUrl = updatedUser.ProfileImageUrl;



            await _context.SaveChangesAsync();
            return existingUser;
        }


        public async Task<Users?> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return null;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}