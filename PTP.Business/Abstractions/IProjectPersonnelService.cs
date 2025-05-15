using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTP.EntityLayer.Models;

namespace PTP.Business.Abstractions
{
    public interface IProjectPersonnelService
    {
        Task<List<Project>> GetProjectsByUserIdAsync(int userId);
        Task<ProjectPersonnel> GetProjectPersonnelByUserIdAndProjectIdAsync(int personnelId, int projectId);
        List<ProjectPersonnel> GetAllWithPersonnelByProjectId(int projectId);
    }
}
