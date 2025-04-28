using PTP.EntityLayer.Abstractions;

namespace PTP.EntityLayer.Models;

public class Project : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<Process> Processes { get; set; } = new List<Process>();
}
