using System.ComponentModel.DataAnnotations;

namespace EgyWonders.DTO
{
    public class TourBookingDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Total price must be greater than 0.")]
        public decimal TotalPrice { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number of participants must be at least 1.")]
        public int NumParticipants { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ScheduleId { get; set; }
    }
}
