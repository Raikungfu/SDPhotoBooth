namespace BussinessObject.Models
{
    public class PhotoHistory : BaseEntityNoAudit
    {
        public string? CustomerId { get; set; }
        public int? BoothId { get; set; }
        public decimal Amount { get; set; }
        public string SessionCode { get; set; }
        
        public Session Session { get; set; }
        public User Customer { get; set; }
        public Booth Booth { get; set; }
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
