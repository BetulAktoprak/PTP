using Microsoft.EntityFrameworkCore;
using PTP.DataAccess;
using PTP.EntityLayer.Models;

namespace PTP.Business.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUserAsync(User user, string password)
        {
            user.Password = password;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
        public User ValidateUser(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password)!;
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
