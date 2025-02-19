
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService : ICrud<User>
    {
        public Task<ICollection<User>> GetAllWithDetailsAsync();
        public Task<User> GetWithDetailsByIdAsync(string id);
        public Task<User> LoginAsync(string email, string password);
        public Task<User> RegisterAsync(User entity);
    }
}
