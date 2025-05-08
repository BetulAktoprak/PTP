using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTP.EntityLayer.Models;

namespace PTP.DataAccess.Repositories
{
    public class ProjectPersonnelRepository : GenericRepository<ProjectPersonnel>
    {
        public ProjectPersonnelRepository(AppDbContext context) : base(context)
        {
        }
    }
}
