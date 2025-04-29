using PTP.EntityLayer.Abstractions;

namespace PTP.EntityLayer.Models;

public class Personnel : BaseEntity
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Role { get; set; } = "User";
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<Process> Processes { get; set; } = new List<Process>();
}
