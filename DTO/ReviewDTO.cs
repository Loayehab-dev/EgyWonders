namespace EgyWonders.DTO
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int? ListingId { get; set; }
        public int? TourId { get; set; }
    }
}
