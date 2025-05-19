using Microsoft.AspNetCore.Mvc.Rendering;

namespace PTP.UI.Models
{
    public class ProcessAddViewModel
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProcessType { get; set; }
        public int AssignedUserId { get; set; } //Atanacak personel
        public int ProcessStageId { get; set; } //Ait olduğu kolon
        public string Text { get; set; }
        public int ProcessId { get; set; }
        public int PersonnelId { get; set; }
        public string DocumentDescriptions { get; set; }
        public List<IFormFile>? Files { get; set; }

        public List<SelectListItem> ProjectPersonnels { get; set; }

        public List<CommentViewModel> Comments { get; set; } = new();

        // Var olan dokümanlar
        public List<DocumentViewModel> Documents { get; set; } = new();
    }

    public class CommentViewModel
    {
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PersonnelName { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }

    public class DocumentViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string DocumentDescriptions { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
