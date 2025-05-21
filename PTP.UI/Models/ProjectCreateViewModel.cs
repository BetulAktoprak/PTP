using Microsoft.AspNetCore.Mvc.Rendering;
using PTP.EntityLayer.Models;

namespace PTP.UI.Models
{
    public class ProjectCreateViewModel
    {
        public int ProjectId { get; set; }
        public List<IFormFile> ProjectFiles { get; set; } = new();
        public IFormFile? LogoFile { get; set; }
        public string? LogoPath { get; set; }
        public string ProjectTitle { get; set; }
        public string ClientName { get; set; }
        public decimal ProjectRate { get; set; }
        public string ProjectType { get; set; }
        public string Priority { get; set; }
        public string ProjectSize { get; set; }
        public string ProcessStageName { get; set; }
        public DateTime StartingDate { get; set; } = DateTime.Now;
        public DateTime EndingDate { get; set; } = DateTime.Now;
        public string Details { get; set; }
        public string? ExistingFilePath { get; set; }
        public List<string> DocumentDescriptions { get; set; } = new();
        public string SelectedPersonnelIds { get; set; }
        public List<SelectListItem> PersonnelList { get; set; }
        public string PersonnelPermissionsJson { get; set; }
        public List<ProcessStageViewModel> Stages { get; set; } = new();
        public List<DocumentViewModel> ExistingDocuments { get; set; }
        public string DocumentJson { get; set; }
    }
}
