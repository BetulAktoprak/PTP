using PTP.EntityLayer.Abstractions;

namespace PTP.EntityLayer.Models
{
    public class ProjectPersonnel : BaseEntity 
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanComment { get; set; }
    }
}
