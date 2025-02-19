using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User entity);
        Task DeleteAsync(string id);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task UpdateAsync(User entity);
        Task<ICollection<User>> GetAllWithDetailsAsync();
        Task<User> GetWithDetailsByIdAsync(string id);
    }
}
