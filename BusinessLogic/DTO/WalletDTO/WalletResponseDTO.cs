namespace BusinessLogic.DTO.WalletDTO
{
    public class WalletResponseDTO
    {
        public decimal Balance { get; set; } = 0;
        public bool IsLocked { get; set; } = false;
    }
}
