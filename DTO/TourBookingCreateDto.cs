using System.ComponentModel.DataAnnotations;

namespace EgyWonders.DTO
{
    public class CreateTourBookingDTO
    {
        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public int NumParticipants { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ScheduleId { get; set; }
    }
}
