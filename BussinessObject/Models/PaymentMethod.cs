namespace BussinessObject.Models
{
    public class PaymentMethod : BaseEntity
    {
        public string MethodName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Transaction> Transactions { get; set; } 

    }
}
