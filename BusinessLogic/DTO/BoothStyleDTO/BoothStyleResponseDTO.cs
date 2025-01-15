namespace BusinessLogic.DTO.BoothStyleDTO
{
    public class BoothStyleResponseDTO
    {
        public int Id { get; set; }
        public string StyleName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedById { get; set; }
        public string LastModifiedById { get; set; }

    }
}
