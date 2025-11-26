using System.ComponentModel.DataAnnotations;

namespace EgyWonders.DTO
{
    public class TourBookingUpdateDTO
    {
        [Range(1, int.MaxValue)]
        public int NumParticipants { get; set; }

        [Range(0.0, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }
    }
}
