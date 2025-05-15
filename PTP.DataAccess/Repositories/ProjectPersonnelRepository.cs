using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PTP.DataAccess.Abstractions;
using PTP.EntityLayer.Models;

namespace PTP.DataAccess.Repositories
{
    public class ProjectPersonnelRepository : GenericRepository<ProjectPersonnel>, IProjectPersonnelRepository
    {
        private readonly AppDbContext _context;
        public ProjectPersonnelRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetProjectsByUserIdAsync(int userId)
        {
            return await _context.ProjectPersonnels
            .Include(pp => pp.Project)
            .Where(pp => pp.Personnel.UserId == userId)
            .Select(pp => pp.Project)
            .ToListAsync();
        }
        public async Task<ProjectPersonnel> GetProjectPersonnelByUserIdAndProjectIdAsync(int personnelId, int projectId)
        {
            return await _context.ProjectPersonnels
               .Include(pp => pp.Personnel)
               .Include(pp => pp.Project)
               .FirstOrDefaultAsync(pp => pp.PersonnelId == personnelId && pp.ProjectId == projectId);
        }
        public List<ProjectPersonnel> GetAllWithPersonnelByProjectId(int projectId)
        {
            return _context.ProjectPersonnels
                .Include(pp => pp.Personnel)
                .Where(pp => pp.ProjectId == projectId)
                .ToList();
        }

    }
}
