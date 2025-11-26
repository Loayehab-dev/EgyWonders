using System.ComponentModel.DataAnnotations;

namespace EgyWonders.DTO
{
    public class ConfirmEmailDTO
    {
        [Required]
        public string Token { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
