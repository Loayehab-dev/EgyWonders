using System.ComponentModel.DataAnnotations;

namespace EgyWonders.DTO
{
    public class ListingPhotoDTO
    {
        public int? PhotoId { get; set; }
        public string Url { get; set; } = null!;
        public string? Caption { get; set; }
        public int ListingId { get; set; }
    }
}
