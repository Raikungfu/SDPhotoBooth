namespace BussinessObject.Models
{
    public class MembershipCard : BaseEntity
    {
        public string CustomerId { get; set; }
        public int LevelMemberShipId { get; set; }
        public int Points { get; set; } = 0;
        public DateTime Expired { get; set; } = DateTime.Now.AddYears(1);
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public User Customer { get; set; }
        public LevelMemberShip LevelMemberShip { get; set; }
    }
}
