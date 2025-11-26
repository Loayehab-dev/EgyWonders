namespace EgyWonders.DTO
{
    public class ListingBookingDTO
    {
        public int BookingId { get; set; }
        public int ListingId { get; set; }
        public int UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
