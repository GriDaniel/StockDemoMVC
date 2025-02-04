using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StockDemo.Data;
using StockDemo.Models;

namespace StockDemo.Services
{
    public class StockService : StockInterface
    {

        public readonly IMongoDatabase _db;

        public StockService(IOptions<DBConnectionSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.DatabaseName);
          
        }
        public IMongoCollection<Stock> stockCollection => _db.GetCollection<Stock>("StockCollection");

        public IEnumerable<Stock> GetStocks()
        {
            return stockCollection.Find(a => true).ToList();

        }
        public Stock GetStock(string name)
        {
            var stock = stockCollection.Find(c => c.Name == name).FirstOrDefault();
            return stock;
        }
        public void Create(Stock stock)
        {
            stockCollection.InsertOne(stock);
        }

        public void Delete(string name)
        {
            var filter = Builders<Stock>.Filter.Eq(m => m.Name, name);
            stockCollection.DeleteOne(filter);
        }


        public void Update(string _id, Stock stock)
        {
            var filter = Builders<Stock>.Filter.Eq(q => _id, _id);
            var update = Builders<Stock>.Update.Set("Name", stock.Name).Set("Price", stock.Price);
            stockCollection.UpdateOne(filter, update);
        }
    }
}
