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

        public async Task<Personnel> GetPersonnelByUserIdAsync(int userId)
        {
            return await _context.Personnels.FirstOrDefaultAsync(p => p.UserId == userId);
        }

    }
}
