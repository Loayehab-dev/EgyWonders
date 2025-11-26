namespace EgyWonders.DTO
{
    public class CreateListingBookingDTO
    {
        public int ListingId { get; set; }
        public int UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
       
    }
}
