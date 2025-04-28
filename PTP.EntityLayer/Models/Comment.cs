using PTP.EntityLayer.Abstractions;

namespace PTP.EntityLayer.Models;

public class Comment : BaseEntity
{
    public string? Text { get; set; }
    public int ProcessId { get; set; }
    public Process? Process { get; set; }
    public int PersonnelId { get; set; }
    public Personnel? Personnel { get; set; }
}
