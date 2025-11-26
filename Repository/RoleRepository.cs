using Microsoft.EntityFrameworkCore;
using EgyWonders.IRepository;
using EgyWonders.Models;
using EgyWonders.Data;

namespace EgyWonders.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TravelDbContext _context;

        public RoleRepository(TravelDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetByIdAsync(int roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == name);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> CreateAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role?> UpdateAsync(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return role;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExistsAsync(role.RoleId))
                    return null;
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role == null)
                return false;

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int roleId)
        {
            return await _context.Roles.AnyAsync(r => r.RoleId == roleId);
        }

        public async Task<bool> RoleNameExistsAsync(string name)
        {
            return await _context.Roles.AnyAsync(r => r.RoleName == name);
        }

        public async Task<bool> AssignRoleToUserAsync(int userId, int roleId)
        {
            if (await UserHasRoleAsync(userId, roleId))
                return false;

            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId,
                AssignedAt = DateTime.UtcNow
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRoleFromUserAsync(int userId, int roleId)
        {
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (userRole == null)
                return false;

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserHasRoleAsync(int userId, int roleId)
        {
            return await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }
    }
}
