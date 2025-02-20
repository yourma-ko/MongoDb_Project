using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class OrderItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; }

        [BsonIgnore]
        public Order Order { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public string Title { get; set; }

        [BsonIgnore]
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [BsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
