
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICartRepository
    {
        public Task<Cart> GetCartByCustomerIdAsync(string customerId);
        public Task<ICollection<Cart>> GetAllCartsAsync();
        public Task AddCartAsync(Cart cart);
        public Task UpdateCartAsync(Cart cart);
        public Task DeleteCartAsync(string cartId);

    }
}
