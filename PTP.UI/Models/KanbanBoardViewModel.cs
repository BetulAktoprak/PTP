using PTP.EntityLayer.Models;

namespace PTP.UI.Models;

public class KanbanBoardViewModel
{
    public int ProjectId { get; set; }
    public List<Process> ToDo { get; set; } = new();
    public List<Process> Working { get; set; } = new();
    public List<Process> Done { get; set; } = new();
}
