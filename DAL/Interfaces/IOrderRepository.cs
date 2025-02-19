using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order entity);
        Task DeleteAsync(string id);
        Task<List<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(string id);
        Task UpdateAsync(Order entity);
    }
}
