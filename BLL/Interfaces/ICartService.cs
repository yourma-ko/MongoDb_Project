
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICartService
    {
        public Task<Cart> getCartByCustomerIdAsync(string customerId);
        public Task AddItemToCartAsync(string customerId, CartItem item);
        public Task RemoveItemFromCartAsync(string customerId, CartItem item);
        public Task<Order> CheckoutFromCartAsync(string customerId);
        public Task ClearCartAsync(string customerId);
        public Task<Decimal> CalculateTotal(string customerId);
        public Task ChangeQuantityAsync(string customerId, CartItem item, int delta);
        public Task CheckItemAsync(string customerId, string productId, bool isChecked);


    }
}
