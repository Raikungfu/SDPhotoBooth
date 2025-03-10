using BusinessLogic.DTO.DashboardDTO;
using BusinessLogic.DTO.OrderDTO;

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
        Task<TotalOrderStaticResponseDTO> StaticOrderCreated(StaticType staticType);
        Task<TotalRevenueStaticResponseDTO> StaticRevenue(StaticType staticType);
    }
}
