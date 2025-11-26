using System.ComponentModel.DataAnnotations;

namespace EgyWonders.DTO
{
    public class LoginDTO
    {
        [Required]
        public string UsernameOrEmail { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
