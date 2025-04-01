using AutoMapper;
using BusinessLogic.DTO.DashboardDTO;
using BusinessLogic.DTO.OrderDTO;
using BusinessLogic.Service.IService;
using BusinessLogic.Utils;
using BussinessObject.Enums;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;

namespace BusinessLogic.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OrderResponseDTO> CreateOrder(OrderRequestDTO orderRequestDTO)
        {
            var order = _mapper.Map<Order>(orderRequestDTO);
            _unitOfWork.Order.Add(order);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderResponseDTO>(order);
        }

        public async Task<OrderResponseDTO> GetOrder(int id, string? userId)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(
                includeProperties: "User,Payment,Payment.PaymentMethod,Session,Coupon,TypeSession,Session.PhotoHistory.Booth,Session.PhotoHistory.Booth.Location",
                filter: e => e.Id == id && (userId == null || e.UserId == userId),
                asNoTracking: true
            );

            return _mapper.Map<OrderResponseDTO>(order);
        }

        public async Task<OrderResponseDTO> GetOrderByCode(long code, string? userId)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(
                includeProperties: "User,Payment,Payment.PaymentMethod,Session,Coupon,TypeSession,Session.PhotoHistory.Booth,Session.PhotoHistory.Booth.Location",
                filter: e => e.Code == code && (userId == null || e.UserId == userId),
                asNoTracking: true
            );

            return _mapper.Map<OrderResponseDTO>(order);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetAllOrders()
        {
            var orders = await _unitOfWork.Order.GetAllAsync(
                includeProperties: "User,Payment,Payment.PaymentMethod,Session,Coupon,TypeSession,Session.PhotoHistory.Booth,Session.PhotoHistory.Booth.Location",
                asNoTracking: true);

            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetOrdersForCurrentUser(string userId)
        {
            var orders = await _unitOfWork.Order.GetAllAsync(
                includeProperties: "User,Payment,Payment.PaymentMethod,Session,Coupon,TypeSession,Session.PhotoHistory.Booth,Session.PhotoHistory.Booth.Location", filter: e => e.UserId == userId, asNoTracking: true);

            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetOrdersByCusId(string userId)
        {
            var orders = await _unitOfWork.Order.GetAllAsync(
                includeProperties: "User,Payment,Payment.PaymentMethod,Session,Coupon,TypeSession,Session.PhotoHistory.Booth,Session.PhotoHistory.Booth.Location", 
                filter: e => e.UserId == userId, asNoTracking: true);

            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<long> DeleteOrder(long id)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            _unitOfWork.Order.Remove(order);
            await _unitOfWork.SaveAsync();
            return id;
        }

        public async Task<OrderResponseDTO> UpdateOrder(int id, OrderRequestDTO orderRequestDTO)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(
                includeProperties: "User,Payment,Payment.PaymentMethod,Session,Coupon,TypeSession,Session.PhotoHistory.Booth,Session.PhotoHistory.Booth.Location", 
                filter: e => e.Id == id);
            order = _mapper.Map(orderRequestDTO, order);
            _unitOfWork.Order.Update(order);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderResponseDTO>(order);
        }

        public async Task<OrderResponseDTO> UpdateOrderStatus(OrderStatusRequestDTO orderStatusRequestDTO)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(filter: e => e.Id == orderStatusRequestDTO.OrderId);
            if (order == null)
            {
                return null;
            }

            order.Status = orderStatusRequestDTO.Status;
            _unitOfWork.Order.Update(order);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderResponseDTO>(order);
        }

        public async Task<OrderResponseDTO> UpdateOrderStatusByCode(OrderStatusCodeRequestDTO orderStatusRequestDTO)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(filter: e => e.Code == orderStatusRequestDTO.OrderCode);
            if(order == null)
            {
                return null;
            }
            order.Status = orderStatusRequestDTO.Status;
            _unitOfWork.Order.Update(order);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderResponseDTO>(order);
        }

        public async Task<TotalOrderStaticResponseDTO> StaticOrderCreated()
        {
            var totalOrderStatic = new TotalOrderStaticResponseDTO();

            var today = DateTime.Today;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);
            var startOfPrevMonth = startOfMonth.AddMonths(-1);
            var endOfPrevMonth = startOfMonth;

            totalOrderStatic.TotalOrder = await _unitOfWork.Order.CountAsync(s => s.CreatedAt >= startOfMonth && s.Status == OrderStatus.Completed);
            totalOrderStatic.TotalOrderPrev = await _unitOfWork.Order.CountAsync(s => s.CreatedAt >= startOfPrevMonth && s.CreatedAt < endOfPrevMonth && s.Status == OrderStatus.Completed);

            return totalOrderStatic;
        }

        public async Task<TotalOrderStaticResponseDTO> StaticOrderCreated(StaticType staticType)
        {
            var totalOrderStatic = new TotalOrderStaticResponseDTO();
            (DateTime start, DateTime startPrev) = TimeRangeHelper.GetTimeRange(staticType);

            totalOrderStatic.TotalOrder = await _unitOfWork.Order.CountAsync(s => s.CreatedAt >= start && s.CreatedAt < start.Add(TimeRangeHelper.GetStepSize(staticType)) && s.Status == OrderStatus.Completed);
            totalOrderStatic.TotalOrderPrev = await _unitOfWork.Order.CountAsync(s => s.CreatedAt >= startPrev && s.CreatedAt < start && s.Status == OrderStatus.Completed);

            return totalOrderStatic;
        }

        public async Task<TotalRevenueStaticResponseDTO> StaticRevenue(StaticType staticType)
        {
            var totalRevenueStatic = new TotalRevenueStaticResponseDTO();
            (DateTime start, DateTime startPrev) = TimeRangeHelper.GetTimeRange(staticType);

            totalRevenueStatic.TotalRevenue = (await _unitOfWork.Order.GetAllAsync(s => s.CreatedAt >= start && s.CreatedAt < start.Add(TimeRangeHelper.GetStepSize(staticType)) && s.Status == OrderStatus.Completed)).Sum(o => o.Amount);
            totalRevenueStatic.TotalRevenuePrev = (await _unitOfWork.Order.GetAllAsync(s => s.CreatedAt >= startPrev && s.CreatedAt < start && s.Status == OrderStatus.Completed)).Sum(o => o.Amount);

            return totalRevenueStatic;
        }
    }
}
