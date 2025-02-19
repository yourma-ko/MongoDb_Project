using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Delivered,
        Cancelled
    }
    public class Order 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        [BsonRepresentation(BsonType.String)]
        public string CustomerId { get; set; }
        public virtual User Customer { get; set; }


        public decimal TotalPrice { get; set; }
   
        public DateTime OrderDateTime { get; set; }

        public OrderStatus Status { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal Total { get; set; }

    }
}
