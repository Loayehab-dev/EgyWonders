using EgyWonders.Models;

namespace EgyWonders.DTO
{
    public class ListingDTO
    {

        public string? Status { get; set; }

        public int Capacity { get; set; }

        public decimal PricePerNight { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Description { get; set; }

        public string Title { get; set; } = null!;

        public string? Category { get; set; }

        public string? CityName { get; set; }   



    }
}
