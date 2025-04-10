namespace BusinessLogic.DTO.TypeSessionDTO
{
    public class TypeSessionRequestDTO
    {
        public string? ProductId { get; set; } = null;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public decimal Price { get; set; }
        public bool isPrinting { get; set; } 
        public int AbleTakenNumber { get; set; }

        public bool? ForMobile { get; set; } = false;

        public int? LevelMembershipId { get; set; }
    }
}
