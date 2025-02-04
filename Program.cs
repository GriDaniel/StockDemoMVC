using System.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using StockDemo.Data;
using StockDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//bind the values in JSON to model class for the DB connection
builder.Services.Configure<DBConnectionSettings>(builder.Configuration.GetSection("DBConfig"));
builder.Services.AddTransient<StockInterface, StockService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// Get MongoDbSettings from configuration
var dbConnectionSettings = builder.Configuration.GetSection("DBConfig").Get<DBConnectionSettings>();


try
{
    var mongoClient = new MongoClient(dbConnectionSettings.ConnectionString);
    var database = mongoClient.GetDatabase("StockDB");
    var collectionNames = database.ListCollectionNames().ToList();
    // Iterate through each collection and delete all documents
    foreach (var collectionName in collectionNames)
    {
        var collection = database.GetCollection<BsonDocument>(collectionName);
        collection.DeleteMany(Builders<BsonDocument>.Filter.Empty);  // Deletes all documents in the collection
    }
    Console.WriteLine("MongoDB connection successful.");
}
catch (Exception ex)
{
    Console.WriteLine("MongoDB connection unsuccessful. ");
}




app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
