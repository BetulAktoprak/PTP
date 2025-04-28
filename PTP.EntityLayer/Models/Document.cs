using PTP.EntityLayer.Abstractions;

namespace PTP.EntityLayer.Models;

public class Document : BaseEntity
{
    public string? FileName { get; set; }
    public string? FilePath { get; set; }
    public int ProcessId { get; set; }
    public Process? Process { get; set; }
    public int PersonnelId { get; set; }
    public Personnel? Personnel { get; set; }
}
