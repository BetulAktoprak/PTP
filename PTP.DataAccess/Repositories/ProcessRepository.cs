using Microsoft.EntityFrameworkCore;
using PTP.DataAccess.Abstractions;
using PTP.EntityLayer.Models;

namespace PTP.DataAccess.Repositories
{
    public class ProcessRepository : GenericRepository<Process>, IProcessRepository
    {
        private readonly AppDbContext _context;
        public ProcessRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Process>> GetAllByProjectIdAsync(int projectId)
        {
            return await _context.Processes
               .Where(p => p.ProjectId == projectId)
               .Include(p => p.Project)
               .Include(p => p.Personnel)
               .ToListAsync();
        }
    }
}
