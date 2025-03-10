using AutoMapper;
using BusinessLogic.DTO.DashboardDTO;
using BusinessLogic.DTO.OrderDTO;
using BusinessLogic.Service.IService;
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

        public async Task<OrderResponseDTO> GetOrder(long id)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(includeProperties: "TypeSession,User,Session,PaymentMethod,Coupon,Session.TypeSession,Session.Booth,Session.Booth.Location", filter: e => e.Id == id);
            var orderResponseDTO = _mapper.Map<OrderResponseDTO>(order);
            return orderResponseDTO;
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetAllOrders()
        {
            var orders = await _unitOfWork.Order.GetAllAsync(includeProperties: "TypeSession,User,Session,PaymentMethod,Coupon,Session.TypeSession,Session.Booth,Session.Booth.Location");
            var orderResponseDTOs = _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
            return orderResponseDTOs;
        }

        public async Task<long> DeleteOrder(long id)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            _unitOfWork.Order.Remove(order);
            await _unitOfWork.SaveAsync();
            return id;
        }

        public async Task<OrderResponseDTO> UpdateOrder(long id, OrderRequestDTO orderRequestDTO)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(includeProperties: "TypeSession,User,Session,PaymentMethod,Coupon,Session.TypeSession,Session.Booth,Session.Booth.Location", filter: e => e.Id == id);
            order = _mapper.Map(orderRequestDTO, order);
            _unitOfWork.Order.Update(order);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderResponseDTO>(order);
        }

        public async Task<OrderResponseDTO> UpdateOrderStatus(OrderStatusRequestDTO orderStatusRequestDTO)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(filter: e => e.Id == orderStatusRequestDTO.OrderId);
            if(order == null)
            {
                return null;
            }
            order.Status = orderStatusRequestDTO.Status;
            _unitOfWork.Order.Update(order);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderResponseDTO>(order);
        }

        public async Task<TotalOrderStaticResponseDTO> StaticOrderCreated(StaticType staticType)
        {
            var totalOrderStatic = new TotalOrderStaticResponseDTO();

            var today = DateTime.Today;

            switch (staticType)
            {
                case StaticType.Day:
                    totalOrderStatic.TotalOrder = await _unitOfWork.Order.CountAsync(s => s.CreatedAt >= today && s.CreatedAt < today.AddDays(1) && s.Status == OrderStatus.Completed);
                    totalOrderStatic.TotalOrderPrev = await _unitOfWork.Order.CountAsync(s => s.CreatedAt >= today.AddDays(-1) && s.CreatedAt < today && s.Status == OrderStatus.Completed);
                    break;

                case StaticType.Week:
                    var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
                    var startOfPrevWeek = startOfWeek.AddDays(-7);
                    totalOrderStatic.TotalOrder = await _unitOfWork.Order.CountAsync(s => s.CreatedAt >= startOfWeek && s.Status == OrderStatus.Completed);
                    totalOrderStatic.TotalOrderPrev = await _unitOfWork.Order.CountAsync(s => s.CreatedAt >= startOfPrevWeek && s.CreatedAt < startOfWeek && s.Status == OrderStatus.Completed);
                    break;

                case StaticType.Month:
                    var startOfMonth = new DateTime(today.Year, today.Month, 1);
                    var startOfPrevMonth = startOfMonth.AddMonths(-1);
                    totalOrderStatic.TotalOrder = await _unitOfWork.Order.CountAsync(s => s.CreatedAt >= startOfMonth && s.Status == OrderStatus.Completed);
                    totalOrderStatic.TotalOrderPrev = await _unitOfWork.Order.CountAsync(s => s.CreatedAt >= startOfPrevMonth && s.CreatedAt < startOfMonth && s.Status == OrderStatus.Completed);
                    break;

                case StaticType.Year:
                    var startOfYear = new DateTime(today.Year, 1, 1);
                    var startOfPrevYear = startOfYear.AddYears(-1);
                    totalOrderStatic.TotalOrder = await _unitOfWork.Order.CountAsync(s => s.CreatedAt >= startOfYear && s.Status == OrderStatus.Completed);
                    totalOrderStatic.TotalOrderPrev = await _unitOfWork.Order.CountAsync(s => s.CreatedAt >= startOfPrevYear && s.CreatedAt < startOfYear && s.Status == OrderStatus.Completed);
                    break;
            }

            return totalOrderStatic;
        }

        public async Task<TotalRevenueStaticResponseDTO> StaticRevenue(StaticType staticType)
        {
            var totalRevenueStatic = new TotalRevenueStaticResponseDTO();

            var today = DateTime.Today;

            switch (staticType)
            {
                case StaticType.Day:
                    totalRevenueStatic.TotalRevenue = (await _unitOfWork.Order.GetAllAsync(s => s.CreatedAt >= today && s.CreatedAt < today.AddDays(1) && s.Status == OrderStatus.Completed, asNoTracking: false)).Sum(o => o.Amount);
                    totalRevenueStatic.TotalRevenuePrev = (await _unitOfWork.Order.GetAllAsync(s => s.CreatedAt >= today.AddDays(-1) && s.CreatedAt < today && s.Status == OrderStatus.Completed, asNoTracking: false)).Sum(o => o.Amount);
                    break;

                case StaticType.Week:
                    var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
                    var startOfPrevWeek = startOfWeek.AddDays(-7);
                    totalRevenueStatic.TotalRevenue = (await _unitOfWork.Order.GetAllAsync(s => s.CreatedAt >= startOfWeek && s.Status == OrderStatus.Completed, asNoTracking: false)).Sum(o => o.Amount);
                    totalRevenueStatic.TotalRevenuePrev = (await _unitOfWork.Order.GetAllAsync(s => s.CreatedAt >= startOfPrevWeek && s.CreatedAt < startOfWeek && s.Status == OrderStatus.Completed, asNoTracking: false)).Sum(o => o.Amount);
                    break;

                case StaticType.Month:
                    var startOfMonth = new DateTime(today.Year, today.Month, 1);
                    var startOfPrevMonth = startOfMonth.AddMonths(-1);
                    totalRevenueStatic.TotalRevenue = (await _unitOfWork.Order.GetAllAsync(s => s.CreatedAt >= startOfMonth && s.Status == OrderStatus.Completed, asNoTracking: false)).Sum(o => o.Amount);
                    totalRevenueStatic.TotalRevenuePrev = (await _unitOfWork.Order.GetAllAsync(s => s.CreatedAt >= startOfPrevMonth && s.CreatedAt < startOfMonth && s.Status == OrderStatus.Completed, asNoTracking: false)).Sum(o => o.Amount);
                    break;

                case StaticType.Year:
                    var startOfYear = new DateTime(today.Year, 1, 1);
                    var startOfPrevYear = startOfYear.AddYears(-1);
                    totalRevenueStatic.TotalRevenue = (await _unitOfWork.Order.GetAllAsync(s => s.CreatedAt >= startOfYear && s.Status == OrderStatus.Completed, asNoTracking: false)).Sum(o => o.Amount);
                    totalRevenueStatic.TotalRevenuePrev = (await _unitOfWork.Order.GetAllAsync(s => s.CreatedAt >= startOfPrevYear && s.CreatedAt < startOfYear && s.Status == OrderStatus.Completed, asNoTracking: false)).Sum(o => o.Amount);
                    break;
            }

            return totalRevenueStatic;
        }

    }
}
