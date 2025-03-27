namespace BussinessObject.Models
{
    public class Wallet : BaseEntity
    {
        public string? UserId { get; set; }
        public decimal Balance { get; set; } = 0;
        public User User { get; set; }
        public ICollection<Deposit> Deposits { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
