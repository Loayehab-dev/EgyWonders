using EgyWonders.Models;

namespace EgyWonders.IRepository
{
    public interface ITourRepository
    {
        Task<IEnumerable<Tour>> GetAllAsync();
        Task<Tour> GetByIdAsync(int id);
        Task<IEnumerable<Tour>> GetByCityAsync(string city);
        Task AddAsync(Tour tour);
        Task UpdateAsync(Tour tour);
        Task DeleteAsync(int id);
    }
}
