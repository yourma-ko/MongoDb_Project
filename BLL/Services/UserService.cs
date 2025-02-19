
using BLL.Interfaces;
using BLL.Utilities;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task AddAsync(User entity)
        {
            await userRepository?.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await userRepository?.DeleteAsync(id.ToString());
        }

        public async Task<List<User>> GetAllAsync()
        {
           return await userRepository.GetAllAsync();
        }

        public async Task<ICollection<User>> GetAllWithDetailsAsync()
        {
            return await userRepository.GetAllWithDetailsAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await userRepository.GetByIdAsync(id.ToString());
        }

        public async Task<User> GetWithDetailsByIdAsync( string id)
        {
            return await userRepository.GetWithDetailsByIdAsync(id.ToString());
        }

        public async Task UpdateAsync(User entity)
        {
            await userRepository.UpdateAsync(entity);
        }
        public async Task<User> LoginAsync (string email, string password)
        {
                var users = await userRepository.GetAllAsync();
                var customer = users.Where(u => u.Email == email).FirstOrDefault();
                if (customer == null)
                {
                    throw new Exception("invalid login");
                }
                var hashedPassword = PasswordHasher1.HashPassword(password);
                if (customer.HashedPassword != hashedPassword)
                {
                    throw new Exception("invalid password");
                }
                return customer;
        }
        public async Task<User> RegisterAsync(User entity)
        {
            var users = await userRepository.GetAllAsync() ?? new List<User>();

            var customer = users.Where(u => u.Email == entity.Email).FirstOrDefault();
            if (customer != null)
            {
                throw new Exception("login already exists");
            }
            entity.HashedPassword = PasswordHasher1.HashPassword(entity.HashedPassword);
            await userRepository.AddAsync(entity);
            return entity;
        }
    }
}
