using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using PTP.EntityLayer.Abstractions;

namespace PTP.EntityLayer.Models;

public class Project : BaseEntity
{
    public string? ProjectTitle { get; set; }
    public string? ClientName { get; set; }
    public decimal? ProjectRate { get; set; }
    public string? ProjectType { get; set; }
    public string? Priority { get; set; }
    public string? ProjectSize { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Details { get; set; }
    public string? FilePath { get; set; }
    [NotMapped]
    public IFormFile? ProjectFile { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<Personnel> Personnels { get; set; } = new List<Personnel>();
    public ICollection<Process> Processes { get; set; } = new List<Process>();
}
