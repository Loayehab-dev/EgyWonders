using System.ComponentModel.DataAnnotations;

namespace EgyWonders.DTO
{
    public class ChangePasswordDTO
    {
        [Required]
        public string CurrentPassword { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string NewPassword { get; set; } = null!;

        [Required]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; } = null!;
    }
}
