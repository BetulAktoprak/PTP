using PTP.EntityLayer.Models;

namespace PTP.Business.Abstractions
{
    public interface IPersonnelService
    {
        Task<Personnel> GetPersonnelByUserIdAsync(int userId);
    }
}
