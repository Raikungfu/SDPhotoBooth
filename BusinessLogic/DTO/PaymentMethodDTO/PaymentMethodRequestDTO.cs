namespace BusinessLogic.DTO.PaymentMethod
{
    public class PaymentMethodRequestDTO
    {
        public string? MethodName { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
