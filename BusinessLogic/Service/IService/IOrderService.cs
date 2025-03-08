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
        Task<int> DeleteOrder(int id);
        Task<OrderResponseDTO> UpdateOrder(int id, OrderRequestDTO locationRequestDTO);
        Task<OrderResponseDTO> UpdateOrderStatus(OrderStatusRequestDTO orderStatusRequestDTO);
        Task<OrderResponseDTO> GetOrder(int id);
        Task<IEnumerable<OrderResponseDTO>> GetAllOrders();
    }
}
