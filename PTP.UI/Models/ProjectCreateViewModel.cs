using Microsoft.AspNetCore.Mvc.Rendering;

namespace PTP.UI.Models
{
    public class ProjectCreateViewModel
    {
        public int Id { get; set; }
        public string ProjectTitle { get; set; }
        public string ClientName { get; set; }
        public decimal ProjectRate { get; set; }
        public string ProjectType { get; set; }
        public string Priority { get; set; }
        public string ProjectSize { get; set; }
        public DateTime StartingDate { get; set; } = DateTime.Now;
        public DateTime EndingDate { get; set; } = DateTime.Now;
        public string Details { get; set; }
        public string DocumentDetail { get; set; }
        public string? ExistingFilePath { get; set; }
        public List<IFormFile> ProjectFiles { get; set; }
        public string SelectedPersonnelIds { get; set; }
        public List<SelectListItem> PersonnelList { get; set; }
        public string PersonnelPermissionsJson { get; set; }
    }
}
