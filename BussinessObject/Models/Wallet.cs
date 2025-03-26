namespace BussinessObject.Models
{
    public class Wallet : BaseEntity
    {
        public string? UserId { get; set; }
        public decimal Balance { get; set; } = 0;
        public virtual User User { get; set; }
        public virtual ICollection<Deposit> Deposits { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
