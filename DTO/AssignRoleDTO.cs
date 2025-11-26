using System.ComponentModel.DataAnnotations;

namespace EgyWonders.DTO
{
    public class AssignRoleDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
