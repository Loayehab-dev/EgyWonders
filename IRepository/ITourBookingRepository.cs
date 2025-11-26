using EgyWonders.Models;

namespace EgyWonders.IRepository
{
    public interface ITourBookingRepository
    {
        Task<IEnumerable<TourBooking>> GetAllAsync();
        Task<TourBooking?> GetByIdAsync(int id);
        Task AddAsync(TourBooking booking);
        Task UpdateAsync(TourBooking booking);
        Task DeleteAsync(int id);
    }
}
