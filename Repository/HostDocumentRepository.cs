using Microsoft.EntityFrameworkCore;
using EgyWonders.IRepository;
using EgyWonders.Models;
using EgyWonders.Data;

namespace EgyWonders.Repository
{
    public class HostDocumentRepository: IHostDocumentRepository
    {
        private readonly TravelDbContext _context;

        public HostDocumentRepository(TravelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HostDocument>> GetAllAsync() => await _context.HostDocuments.AsNoTracking().ToListAsync();

        public async Task<HostDocument> GetByIdAsync(int id) => await _context.HostDocuments.FindAsync(id);

        public async Task<IEnumerable<HostDocument>> GetByUserIdAsync(int userId) =>
            await _context.HostDocuments.Where(d => d.UserId == userId).AsNoTracking().ToListAsync();

        public async Task AddAsync(HostDocument document)
        {
            await _context.HostDocuments.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HostDocument document)
        {
            _context.HostDocuments.Update(document);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var document = await _context.HostDocuments.FindAsync(id);
            if (document != null)
            {
                _context.HostDocuments.Remove(document);
                await _context.SaveChangesAsync();
            }
        }
    }
}
