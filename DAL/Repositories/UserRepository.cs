using DAL.Interfaces;
using DAL.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> userCollection;

        public UserRepository(IMongoDatabase database)
        {
            userCollection = database.GetCollection<User>("users");
        }

        public async Task AddAsync(User entity)
        {
            await userCollection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await userCollection.DeleteOneAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await userCollection.Find(_ => true).ToListAsync();
            Console.WriteLine(users);
            return users;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await userCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            var update = Builders<User>.Update
                .Set(u => u.Orders, entity.Orders); 

            await userCollection.UpdateOneAsync(u => u.Id == entity.Id, update);
        }

        public async Task<ICollection<User>> GetAllWithDetailsAsync()
        {
            return await userCollection.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetWithDetailsByIdAsync(string id)
        {
            return await userCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public Task<User> GetWithDetailsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
