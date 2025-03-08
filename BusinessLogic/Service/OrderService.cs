using AutoMapper;
using BusinessLogic.DTO.OrderDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    class OrderService : IOrderService
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

        public async Task<OrderResponseDTO> GetOrder(int id)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            var orderResponseDTO = _mapper.Map<OrderResponseDTO>(order);
            return orderResponseDTO;
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetAllOrders()
        {
            var orders = await _unitOfWork.Order.GetAllAsync();
            var orderResponseDTOs = _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
            return orderResponseDTOs;
        }

        public async Task<int> DeleteOrder(int id)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            _unitOfWork.Order.Remove(order);
            await _unitOfWork.SaveAsync();
            return id;
        }

        public async Task<OrderResponseDTO> UpdateOrder(int id, OrderRequestDTO orderRequestDTO)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            order = _mapper.Map(orderRequestDTO, order);
            _unitOfWork.Order.Update(order);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderResponseDTO>(order);
        }

        public async Task<OrderResponseDTO> UpdateOrderStatus(OrderStatusRequestDTO orderStatusRequestDTO)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(filter: e => e.Id == orderStatusRequestDTO.OrderId);
            order.Status = orderStatusRequestDTO.Status;
            _unitOfWork.Order.Update(order);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderResponseDTO>(order);
        }
    }
}
