using Microsoft.EntityFrameworkCore;
using EgyWonders.IRepository;
using EgyWonders.Models;
using EgyWonders.Data;

namespace EgyWonders.IRepository
{
    public interface ITourSchedulesRepository
    {
        Task<IEnumerable<TourSchedule>> GetAllAsync();
        Task<TourSchedule> GetByIdAsync(int id);
        Task<IEnumerable<TourSchedule>> GetByTourIdAsync(int tourId);
        Task AddAsync(TourSchedule schedule);
        Task UpdateAsync(TourSchedule schedule);
        Task DeleteAsync(int id);
    }
}
