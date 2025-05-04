using PTP.EntityLayer.Models;

namespace PTP.DataAccess.Abstractions;
public interface IProcessRepository
{
    Task<List<Process>> GetAllByProjectIdAsync(int projectId);
}
