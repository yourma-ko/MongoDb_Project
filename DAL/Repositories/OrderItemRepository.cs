using DAL.Interfaces;
using DAL.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly IMongoCollection<OrderItem> orderItemCollection;
        private readonly IMongoCollection<Product> productCollection;

        public OrderItemRepository(IMongoDatabase database)
        {
            orderItemCollection = database.GetCollection<OrderItem>("orderItems");
            productCollection = database.GetCollection<Product>("products");
        }

        public async Task AddAsync(OrderItem entity)
        {
            await orderItemCollection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await orderItemCollection.DeleteOneAsync(o => o.Id == id);
        }

        public async Task<List<OrderItem>> GetAllAsync()
        {
            return await orderItemCollection.Find(_ => true).ToListAsync();
        }

        public async Task<ICollection<OrderItem>> GetAllWithDetailsAsync()
        {
            var orderItems = await orderItemCollection.Find(_ => true).ToListAsync();
            foreach (var item in orderItems)
            {
                item.Product = await productCollection.Find(p => p.Id == item.ProductId).FirstOrDefaultAsync();
            }
            return orderItems;
        }

        public async Task<OrderItem> GetByIdAsync(string id)
        {
            return await orderItemCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<OrderItem> GetWithDetailsByIdAsync(string id)
        {
            var orderItem = await orderItemCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
            if (orderItem != null)
            {
                orderItem.Product = await productCollection.Find(p => p.Id == orderItem.ProductId).FirstOrDefaultAsync();
            }
            return orderItem;
        }


        public async Task UpdateAsync(OrderItem entity)
        {
            await orderItemCollection.ReplaceOneAsync(o => o.Id == entity.Id, entity);
        }
    }
}
