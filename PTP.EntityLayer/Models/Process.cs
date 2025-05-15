using Microsoft.AspNetCore.Http;
using PTP.EntityLayer.Abstractions;

namespace PTP.EntityLayer.Models;

public class Process : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int ProcessStageId { get; set; }
    public ProcessStage ProcessStage { get; set; } = null!;
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public int PersonnelId { get; set; }
    public Personnel? Personnel { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Document> Documents { get; set; } = new List<Document>();
}
