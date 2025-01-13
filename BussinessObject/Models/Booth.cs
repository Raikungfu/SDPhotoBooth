using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BussinessObject.Models
{
    public class Booth : BaseEntity
    {
        [Required]
        public int LocationId { get; set; }

        [Required]
        [StringLength(100)]
        public string? BoothName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool Status { get; set; } = true;

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public ICollection<PhotoHistory> PhotoHistories { get; set; } = new List<PhotoHistory>();

        public ICollection<Session> Sessions { get; set; } = new List<Session>();

        //Thêm FK 1 booth có 1 Style (BoothStyle) thêm bảng BoothStyle



    }
}