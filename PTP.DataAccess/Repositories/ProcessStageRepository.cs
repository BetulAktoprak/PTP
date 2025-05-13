using PTP.EntityLayer.Models;

namespace PTP.DataAccess.Repositories
{
    public class ProcessStageRepository : GenericRepository<ProcessStage>
    {
        public ProcessStageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
