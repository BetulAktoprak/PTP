using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTP.EntityLayer.Abstractions;

namespace PTP.EntityLayer.Models
{
    public class ProcessPersonnel : BaseEntity
    {
        public int ProcessId { get; set; }
        public Process Process { get; set; }

        public int PersonnelId { get; set; }
        public Personnel Personnel { get; set; }

        public DateTime AssignedDate { get; set; } = DateTime.Now;
    }
}
