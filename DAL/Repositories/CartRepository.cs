using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IMongoCollection<Cart> cartCollection;
        public CartRepository(IMongoDatabase database)
        {
            cartCollection = database.GetCollection<Cart>("cart");
        }
        public async Task AddCartAsync(Cart cart)
        {
            await cartCollection.InsertOneAsync(cart);
        }

        public async Task DeleteCartAsync(string cartId)
        {
            await cartCollection.DeleteOneAsync(c => c.Id == cartId);
        }

        public async Task<ICollection<Cart>> GetAllCartsAsync()
        {
            return await cartCollection.Find(c => true).ToListAsync();
        }

        public async Task<Cart> GetCartByCustomerIdAsync(string customerId)
        {
            return await cartCollection.Find(c => c.CustomerId == customerId).FirstOrDefaultAsync();
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            await cartCollection.ReplaceOneAsync(c => c.Id == cart.Id, cart);
        }
    }
}
