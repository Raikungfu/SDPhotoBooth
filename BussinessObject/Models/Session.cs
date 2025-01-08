using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace BussinessObject.Models
{
    public class Session
    {
        [Key]
        [Required]
        [StringLength(10, ErrorMessage = "Code cannot exceed 10 characters.")]
        public string Code { get; set; } = GenerateCode(10);

        public DateTime? Expired { get; private set; } = DateTime.Now.AddMinutes(60);

        public bool IsActive { get; set; } = true;

        [Required]
        public int TypeSessionId { get; set; }

        [Required]
        public int BoothId { get; set; }

        [ForeignKey("TypeSessionId")]
        public TypeSession TypeSession { get; set; }


        [ForeignKey("BoothId")]
        public Booth Booth { get; set; }

        public PhotoHistory PhotoHistory { get; set; }

        public virtual Order Order { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


        private static string GenerateCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            using var rng = RandomNumberGenerator.Create();
            var result = new StringBuilder(length);
            var buffer = new byte[sizeof(uint)];

            for (var i = 0; i < length; i++)
            {
                rng.GetBytes(buffer);
                var randomValue = BitConverter.ToUInt32(buffer, 0);
                result.Append(chars[(int)(randomValue % chars.Length)]);
            }

            return result.ToString();
        }

    }
}
