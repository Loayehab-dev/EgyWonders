using System.ComponentModel.DataAnnotations;

namespace EgyWonders.DTO
{
    public class HostDocumentDTO
    {
        public int? DocumentId { get; set; }

        [Required(ErrorMessage = "DocumentPath is required.")]
        [StringLength(300, ErrorMessage = "Path is too long.")]
        public string DocumentPath { get; set; } = null!;

        [StringLength(5000, ErrorMessage = "TextRecord can't exceed 5000 characters.")]
        public string? TextRecord { get; set; }

        [RegularExpression(@"^[0-9]{14}$", ErrorMessage = "National ID must be 14 digits.")]
        public string? NationalId { get; set; }

        public bool? Verified { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
