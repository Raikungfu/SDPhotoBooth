using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Enums
{
    public enum OrderStatus
    {
        None = 0,
        Pending,
        Processing,
        Completed,
        Failed,
        Cancelled
    }
}
