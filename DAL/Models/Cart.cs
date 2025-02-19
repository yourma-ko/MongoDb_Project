using DAL.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class Cart
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.String)] // Хранит GUID как строку
    public string CustomerId { get; set; }

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public decimal Total => CartItems.Sum(item => item.Quantity * item.Price);
}
