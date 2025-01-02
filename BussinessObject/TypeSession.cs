namespace BussinessObject
{
    public class TypeSession : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public int? AbleTakenNumber { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}