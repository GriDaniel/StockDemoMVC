using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StockDemo.Models
{
    public class Stock
    {
        [BsonId]
        public required string _id {  get; set; }
        public required string  Name { get; set; }
        public required string Price { get; set; }
    }
}
