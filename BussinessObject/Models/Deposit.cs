namespace BussinessObject.Models
{
    public class Deposit : BaseEntityNoAudit
    {
        public bool IsSuccess { get; set; }
        public decimal Amount { get; set; }
        public Wallet Wallet { get; set; }
    }
}
