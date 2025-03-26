namespace BussinessObject.Models
{
    public class Booth : BaseEntity
    {
        public string BoothName { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; } = true;
        public int LocationId { get; set; }

        public Location Location { get; set; }
        public ICollection<PhotoHistory> PhotoHistories { get; set; } = new List<PhotoHistory>();

    }
}