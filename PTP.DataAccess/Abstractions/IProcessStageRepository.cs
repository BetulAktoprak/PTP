using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTP.EntityLayer.Models;

namespace PTP.DataAccess.Abstractions
{
    public interface IProcessStageRepository
    {
        Task<ProcessStage> GetStagesByProjectAsync(int projectId);
        void AddRange(IEnumerable<ProcessStage> stages);
        Task<List<ProcessStage>> GetAllByProjectIdAsync(int projectId);

    }
}
