
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderItemRepository orderItemRepository;
        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
        }
        public async Task AddAsync(Order entity)
        {
            await orderRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await orderRepository.DeleteAsync(id.ToString());
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await orderRepository.GetAllAsync();
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            return await orderRepository.GetByIdAsync(id.ToString());
        }

        public async Task UpdateAsync(Order entity)
        {
            await orderRepository.UpdateAsync(entity);
        }
        public async Task<ICollection<Order>> GetUserOrdersAsync(string id)
        {
            var orders = await orderRepository.GetAllAsync();
            var filteredOrders = orders.Where(orders => orders.CustomerId == id).ToList();
            var orderItems = await orderItemRepository.GetAllAsync();
            foreach (var order in filteredOrders)
            {
                order.OrderItems = orderItems.Where(oi => oi.OrderId == order.Id).ToList();
            }
            return filteredOrders;
        }
    }
}
