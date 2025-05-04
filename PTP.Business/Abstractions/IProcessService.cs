using PTP.EntityLayer.Models;

namespace PTP.Business.Abstractions;
public interface IProcessService
{
    Task<List<Process>> GetAllByProjectIdAsync(int projectId);
}
