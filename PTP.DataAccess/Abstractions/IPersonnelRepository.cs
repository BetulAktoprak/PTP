using PTP.EntityLayer.Models;

namespace PTP.DataAccess.Abstractions
{
    public interface IPersonnelRepository
    {
        Task<Personnel> GetPersonnelByUserIdAsync(int userId);
    }
}
