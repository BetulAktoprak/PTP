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
        public User ValidateUser(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password)!;
        }
    }
}
