namespace BussinessObject.Models
{

    public class TypeSession : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public bool isPrinting { get; set; } = true;
        public int AbleTakenNumber { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}