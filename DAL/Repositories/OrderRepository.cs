using DAL.Interfaces;
using DAL.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> orderCollection;

        public OrderRepository(IMongoDatabase database)
        {
            orderCollection = database.GetCollection<Order>("orders");
        }

        public async Task AddAsync(Order entity)
        {
            await orderCollection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await orderCollection.DeleteOneAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await orderCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            return await orderCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Order entity)
        {
            await orderCollection.ReplaceOneAsync(o => o.Id == entity.Id, entity);
        }
    }
}
