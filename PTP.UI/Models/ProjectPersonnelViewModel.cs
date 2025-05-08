namespace PTP.UI.Models
{

    public class ProjectPersonnelViewModel
    {
        public string value { get; set; }
        public string name { get; set; }
        public int ProjectId { get; set; }
        public int PersonnelId { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanComment { get; set; }

    }
}
