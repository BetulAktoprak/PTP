namespace PTP.UI.Models
{
    public class DocumentViewModel
    {
        public int? Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string DocumentDescriptions { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public IFormFile File { get; set; }

    }
}
