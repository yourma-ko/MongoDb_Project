using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddAsync(OrderItem entity);
        Task DeleteAsync(string id);
        Task<List<OrderItem>> GetAllAsync();
        Task<ICollection<OrderItem>> GetAllWithDetailsAsync();
        Task<OrderItem> GetByIdAsync(string id);
        Task<OrderItem> GetWithDetailsByIdAsync(string id);
        Task UpdateAsync(OrderItem entity);
    }
}
