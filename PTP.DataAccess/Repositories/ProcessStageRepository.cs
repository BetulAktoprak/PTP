using Microsoft.EntityFrameworkCore;
using PTP.DataAccess.Abstractions;
using PTP.EntityLayer.Models;

namespace PTP.DataAccess.Repositories
{
    public class ProcessStageRepository : GenericRepository<ProcessStage>, IProcessStageRepository
    {
        private readonly AppDbContext _context;
        public ProcessStageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void AddRange(IEnumerable<ProcessStage> stages)
        {
            _context.ProcessStages.AddRange(stages);
            _context.SaveChanges();
        }

        public async Task<List<ProcessStage>> GetAllByProjectIdAsync(int projectId)
        {
            return await _context.ProcessStages
               .Where(p => p.ProjectId == projectId)
               .Include(p => p.Project)
               .ToListAsync();
        }

        public async Task<ProcessStage> GetStagesByProjectAsync(int projectId)
        {
            return await _context.ProcessStages.FirstOrDefaultAsync(x => x.ProjectId == projectId);
        }
    }
}
