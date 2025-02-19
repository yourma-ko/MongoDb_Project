using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public enum Role
    {
        Admin,
        Customer,
        Guest
    }

    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonRequired]
        public string FirstName { get; set; }

        [BsonRequired]
        public string LastName { get; set; }

        [BsonRequired]
        public string Email { get; set; }

        [BsonRequired]
        public string Phone { get; set; }

        [BsonRequired]
        public string HashedPassword { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public Role Role { get; set; }

        [BsonRequired]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonIgnoreIfNull]
        public List<Order> Orders { get; set; } = new List<Order>();
    }

}
