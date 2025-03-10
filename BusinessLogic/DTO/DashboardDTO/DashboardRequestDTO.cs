using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.DashboardDTO
{
    public class DashboardRequestStaticTotalDTO
    {
        public StaticType StaticType { get; set; }
    }

    public enum StaticType
    {
        Day,
        Week,
        Month,
        Year
    }

    public class DashboardRequestDTO
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
