﻿namespace StockDemo.Data
{
    public class DBConnectionSettings
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
        public required string CollectionName { get; set; }
    }
}
