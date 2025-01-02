using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BussinessObject
{
    public class Location : BaseEntity
    {
        public string LocationName { get; set; }
        public string Address { get; set; }

        public ICollection<Booth> Booths { get; set; }
        public ICollection<User> Staffs { get; set; }

        public ICollection<PhotoHistory> PhotoHistories { get; set; }
    }
}