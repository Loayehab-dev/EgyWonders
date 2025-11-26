using Microsoft.EntityFrameworkCore;
using EgyWonders.IRepository;
using EgyWonders.Models;
using EgyWonders.Data;

namespace EgyWonders.Repository
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly TravelDbContext _context;

        public PaymentRepository(TravelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync() => await _context.Payments.AsNoTracking().ToListAsync();

        public async Task<Payment> GetByIdAsync(int id) => await _context.Payments.FindAsync(id);

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
