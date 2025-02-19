using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(Product entity);
        Task DeleteAsync(string id);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string id);
        Task UpdateAsync(Product entity);
    }
}
