using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{ 
    public class Session : BaseEntityNoAudit
    {
        public string Code { get; set; } = GenerateCode(8);
        private DateTime? Expired { get; set; } = DateTime.Now.AddMinutes(60);
        public bool IsActive { get; set; } = true;
        public int TypeSessionId { get; set; }
        public int PhotoHistoryId { get; set; }

        [ForeignKey("TypeSessionId")]
        public TypeSession TypeSession { get; set; }

        [ForeignKey("PhotoHistoryId")]
        public PhotoHistory PhotoHistory { get; set; }

        private static string GenerateCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[random.Next(s.Length)])
                                        .ToArray());
        }
    }
}
