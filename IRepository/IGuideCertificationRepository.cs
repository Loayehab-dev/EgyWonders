using Microsoft.EntityFrameworkCore;
using EgyWonders.Models;
namespace EgyWonders.IRepository
{
    public interface IGuideCertificationRepository
    {
        Task<IEnumerable<GuideCertification>> GetAllAsync();
        Task<GuideCertification> GetByIdAsync(int id);
        Task<IEnumerable<GuideCertification>> GetByUserIdAsync(int userId);
        Task AddAsync(GuideCertification cert);
        Task UpdateAsync(GuideCertification cert);
        Task DeleteAsync(int id);

    }
}
