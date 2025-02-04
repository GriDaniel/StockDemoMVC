using MongoDB.Driver;
using StockDemo.Models;

namespace StockDemo.Services
{
    public interface StockInterface
    {
        IMongoCollection<Stock> stockCollection { get; }
        IEnumerable<Stock> GetStocks();

        Stock GetStock(string name);
        
        void Create(Stock stock);
        void Update(string _id, Stock stock);
        void Delete(string name);

    }
}
