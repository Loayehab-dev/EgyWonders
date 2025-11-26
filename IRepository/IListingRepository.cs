using EgyWonders.Models;
namespace EgyWonders.IRepository
{
    public interface IListingRepository
    {
        Task<IEnumerable<Listing>> GetAllAsync();
        Task<Listing> GetByIdAsync(int id);
        Task<IEnumerable<Listing>> GetByCityAsync(string city);
        Task AddAsync(Listing listing);
        Task UpdateAsync(Listing listing);
        Task DeleteAsync(int id);
    }
}
