using Microsoft.AspNetCore.Mvc.Rendering;

namespace PTP.UI.Models
{
    public class ManageProcessViewModel
    {
        public List<IFormFile> ProjectFiles { get; set; } = new();
        public string ProjectTitle { get; set; }
        public string ClientName { get; set; }
        public string SelectedPersonnelIds { get; set; }
        public List<SelectListItem> PersonnelList { get; set; }
        public DateTime StartingDate { get; set; } = DateTime.Now;
        public DateTime EndingDate { get; set; } = DateTime.Now;
        public string Details { get; set; }
    }
}
