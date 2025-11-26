using EgyWonders.Models;
namespace EgyWonders.IRepository
{
    public interface IListingBookingRepository
    {
        Task<IEnumerable<ListingBooking>> GetAllAsync();
        Task<IEnumerable<ListingBooking>> GetByListingIdAsync(int listingId);

        Task<ListingBooking> GetByIdAsync(int id);
        Task AddAsync(ListingBooking booking);
        Task UpdateAsync(ListingBooking booking);
        Task DeleteAsync(int id);
    }
}
