using System.ComponentModel.DataAnnotations;

namespace EgyWonders.DTO
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
