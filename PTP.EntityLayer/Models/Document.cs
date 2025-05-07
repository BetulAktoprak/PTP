using PTP.EntityLayer.Abstractions;

namespace PTP.EntityLayer.Models;

public class Document : BaseEntity
{
    public string? FileName { get; set; }
    public string? FilePath { get; set; }
    public string? Description { get; set; }
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
    public int? ProcessId { get; set; }
    public Process? Process { get; set; }
    public int? PersonnelId { get; set; }
    public Personnel? Personnel { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
}
