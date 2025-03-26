namespace BussinessObject.Models
{
    public class Location : BaseEntity
    {
        public string LocationName { get; set; }
        public string Address { get; set; }

        public ICollection<Booth> Booths { get; set; } = new List<Booth>();
        public ICollection<User> Staffs { get; set; } = new List<User>();
    }

}