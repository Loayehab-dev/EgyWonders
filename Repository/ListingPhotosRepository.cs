using Microsoft.EntityFrameworkCore;
using EgyWonders.IRepository;
using EgyWonders.Models;
using EgyWonders.Data;

namespace EgyWonders.Repository
{
    public class ListingPhotosRepository: IListingPhotosRepository
    {
        private readonly TravelDbContext _context;

        public ListingPhotosRepository(TravelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ListingPhoto>> GetAllAsync() => await _context.ListingPhotos.AsNoTracking().ToListAsync();

        public async Task<ListingPhoto> GetByIdAsync(int id) => await _context.ListingPhotos.FindAsync(id);

        public async Task<IEnumerable<ListingPhoto>> GetByListingIdAsync(int listingId) =>
            await _context.ListingPhotos.Where(p => p.ListingId == listingId).AsNoTracking().ToListAsync();

        public async Task AddAsync(ListingPhoto photo)
        {
            await _context.ListingPhotos.AddAsync(photo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ListingPhoto photo)
        {
            _context.ListingPhotos.Update(photo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var photo = await _context.ListingPhotos.FindAsync(id);
            if (photo != null)
            {
                _context.ListingPhotos.Remove(photo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
