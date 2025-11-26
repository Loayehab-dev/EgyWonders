using EgyWonders.Models;
namespace EgyWonders.IRepository
{
    public interface IHostDocumentRepository
    {
        Task<IEnumerable<HostDocument>> GetAllAsync();
        Task<HostDocument> GetByIdAsync(int id);
        Task<IEnumerable<HostDocument>> GetByUserIdAsync(int userId);
        Task AddAsync(HostDocument document);
        Task UpdateAsync(HostDocument document);
        Task DeleteAsync(int id);
    }
}
