using Microsoft.EntityFrameworkCore;
using PTP.DataAccess.Abstractions;
using PTP.EntityLayer.Models;

namespace PTP.DataAccess.Repositories
{
    public class PersonnelRepository : GenericRepository<Personnel>, IPersonnelRepository
    {
        private readonly AppDbContext _context;

        public PersonnelRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Personnel> GetPersonnelWithProjectsByUserIdAsync(int userId)
        {
            return await _context.Personnels
            .Include(p => p.Projects)
            .FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
