using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTP.EntityLayer.Models;

namespace PTP.DataAccess.Repositories
{
    public class PersonnelRepository : GenericRepository<Personnel>
    {
        public PersonnelRepository(AppDbContext context) : base(context)
        {
        }
    }
}
