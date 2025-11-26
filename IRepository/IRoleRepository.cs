using EgyWonders.Models;
namespace EgyWonders.IRepository
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(int roleId);
        Task<Role?> GetByNameAsync(string name);
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role> CreateAsync(Role role);
        Task<Role?> UpdateAsync(Role role);
        Task<bool> DeleteAsync(int roleId);
       
        Task<bool> AssignRoleToUserAsync(int userId, int roleId);
        Task<bool> RemoveRoleFromUserAsync(int userId, int roleId);
        Task<bool> UserHasRoleAsync(int userId, int roleId);
    }
}
