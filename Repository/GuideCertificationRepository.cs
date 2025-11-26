using Microsoft.EntityFrameworkCore;
using EgyWonders.IRepository;
using EgyWonders.Models;
using EgyWonders.Data;

namespace EgyWonders.Repository
{
    public class GuideCertificationRepository: IGuideCertificationRepository
    {
        private readonly TravelDbContext _context;

        public GuideCertificationRepository(TravelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GuideCertification>> GetAllAsync() => await _context.GuideCertifications.ToListAsync();

        public async Task<GuideCertification> GetByIdAsync(int id) => await _context.GuideCertifications.FindAsync(id);

        public async Task<IEnumerable<GuideCertification>> GetByUserIdAsync(int userId) =>
            await _context.GuideCertifications.Where(c => c.GuideId == userId).AsNoTracking().ToListAsync();

        public async Task AddAsync(GuideCertification cert)
        {
            await _context.GuideCertifications.AddAsync(cert);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GuideCertification cert)
        {
            _context.GuideCertifications.Update(cert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cert = await _context.GuideCertifications.FindAsync(id);
            if (cert != null)
            {
                _context.GuideCertifications.Remove(cert);
                await _context.SaveChangesAsync();
            }
        }
    }
}

