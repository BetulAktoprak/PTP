using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTP.EntityLayer.Models;

namespace PTP.DataAccess.Repositories
{
    public class ProcessRepository : GenericRepository<Process>
    {
        public ProcessRepository(AppDbContext context) : base(context)
        {
        }
    }
}
