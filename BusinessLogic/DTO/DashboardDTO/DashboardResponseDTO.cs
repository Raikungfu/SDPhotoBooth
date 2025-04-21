using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.DashboardDTO
{
    public class DashboardResponseDTO
    {
        public TotalOrderStaticResponseDTO TotalOrderStatic { get; set; }
        public TotalUserStaticResponseDTO TotalUserStatic { get; set; }
        public TotalRevenueStaticResponseDTO TotalRevenueStatic { get; set; }
        public TotalProfitStaticResponseDTO TotalProfitStatic { get; set; }
    }

    public class TotalOrderStaticResponseDTO
    {
        public int TotalOrder { get; set; }
        public int TotalOrderPrev { get; set; }
    }

    public class TotalDepositStaticResponseDTO
    {
        public int TotalDeposit { get; set; }
        public int TotalDepositPrev { get; set; }
    }

    public class TotalProfitStaticResponseDTO
    {
        public decimal TotalProfit { get; set; }
        public decimal TotalProfitPrev { get; set; }
    }

    public class TotalUserStaticResponseDTO
    {
        public int TotalUser { get; set; }
        public int TotalUserPrev { get; set; }
    }

    public class TotalRevenueStaticResponseDTO
    {
        public decimal TotalRevenue { get; set; }
        public decimal TotalRevenuePrev { get; set; }
    }
}
