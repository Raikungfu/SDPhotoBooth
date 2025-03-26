namespace BussinessObject.Models
{
    public class Photo : BaseEntityNoAudit
    {
        public string Url { get; set; }
        public int PhotoHistoryId { get; set; }
        public int PhotoStyleId { get; set; }

        public PhotoHistory PhotoHistory { get; set; }
        public PhotoStyle PhotoStyle { get; set; }
    }
}
