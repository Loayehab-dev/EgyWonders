namespace EgyWonders.DTO
{
    public class GuideCertificateDTO
    {
       

        public int GuideId { get; set; }

        public string CertificationName { get; set; } = null!;

        public DateOnly IssueDate { get; set; }

        public DateOnly ExpiryDate { get; set; }
    }
}
