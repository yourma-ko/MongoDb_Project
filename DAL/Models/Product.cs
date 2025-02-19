using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonRequired]
        public string Title { get; set; }

        [BsonRequired]
        public decimal Price { get; set; }

        [BsonRequired]
        public int Quantity { get; set; }

        [BsonRequired]
        public string ImageUrl { get; set; }
        [BsonRequired]
        public DateTime CreatedAt { get; set; }

        public IList<string> Characteristics { get; set; } = new List<string>();

        public string Category { get; set; }
    }
}
