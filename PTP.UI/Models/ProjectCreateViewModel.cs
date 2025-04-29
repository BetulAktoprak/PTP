using Microsoft.AspNetCore.Mvc.Rendering;

namespace PTP.UI.Models
{
    public class ProjectCreateViewModel
    {
        public string ProjectTitle { get; set; }
        public string ClientName { get; set; }
        public decimal ProjectRate { get; set; }
        public string ProjectType { get; set; }
        public string Priority { get; set; }
        public string ProjectSize { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string Details { get; set; }
        public IFormFile ProjectFile { get; set; }
        public List<int> SelectedPersonnelIds { get; set; } = new();
        public List<SelectListItem> PersonnelList { get; set; }
    }
}
