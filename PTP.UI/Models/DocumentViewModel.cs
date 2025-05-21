namespace PTP.UI.Models
{
    public class DocumentViewModel
    {
        public int? Id { get; set; }
        public int ProjectId { get; set; }
        public int? ProcessId { get; set; }
        public int? PersonnelId { get; set; }
        public int? UserId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string DocumentDescriptions { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public IFormFile File { get; set; }

    }
}
