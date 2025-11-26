using EgyWonders.Models;
namespace EgyWonders.IRepository
{
    public interface IListingPhotosRepository
    {
        Task<IEnumerable<ListingPhoto>> GetAllAsync();
        Task<ListingPhoto> GetByIdAsync(int id);
        Task<IEnumerable<ListingPhoto>> GetByListingIdAsync(int listingId);
        Task AddAsync(ListingPhoto photo);
        Task UpdateAsync(ListingPhoto photo);
        Task DeleteAsync(int id);
    }
}
