using BusinessLogic.DTO.OrderDTO;
using BussinessObject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.IService
{
    public interface IOrderService
    {
        Task<OrderResponseDTO> CreateOrder(OrderRequestDTO locationRequestDTO);
        Task<long> DeleteOrder(long id);
        Task<OrderResponseDTO> UpdateOrder(long id, OrderRequestDTO locationRequestDTO);
        Task<OrderResponseDTO> UpdateOrderStatus(OrderStatusRequestDTO orderStatusRequestDTO);
        Task<OrderResponseDTO> GetOrder(long id);
        Task<IEnumerable<OrderResponseDTO>> GetAllOrders();
    }
}
