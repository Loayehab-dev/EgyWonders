using EgyWonders.Models;
namespace EgyWonders.IRepository
{
    public interface IAmenitiesRepository
    {
        Task<IEnumerable<Amenity>> GetAllAsync();
        Task<Amenity> GetByIdAsync(int id);
        Task AddAsync(Amenity amenity);
        Task UpdateAsync(Amenity amenity);
        Task DeleteAsync(int id);
    }
}
