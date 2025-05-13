using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTP.EntityLayer.Abstractions;

namespace PTP.EntityLayer.Models
{
    public class ProcessStage : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string ColorHex { get; set; } = "#CCCCCC";
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
        public ICollection<Process> Processes { get; set; } = new List<Process>();
    }
}
