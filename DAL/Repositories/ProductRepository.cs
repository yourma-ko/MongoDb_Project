using DAL.Interfaces;
using DAL.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> productCollection;

        public ProductRepository(IMongoDatabase database)
        {
            productCollection = database.GetCollection<Product>("products");
        }

        public async Task AddAsync(Product entity)
        {
            await productCollection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await productCollection.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await productCollection.Find(_ => true).ToListAsync();
            Console.WriteLine(products);
            return products;
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await productCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            await productCollection.ReplaceOneAsync(p => p.Id == entity.Id, entity);
        }
    }
}
