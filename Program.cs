using System.Configuration;
using MongoDB.Driver;
using StockDemo.Data;
using StockDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//bind the values in JSON to model class for the DB connection
builder.Services.Configure<DBConnectionSettings>(builder.Configuration.GetSection("DBConfig"));
builder.Services.AddTransient<StockInterface, StockService>();
// Get MongoDbSettings from configuration
var dbConnectionSettings = builder.Configuration.GetSection("DBConfig").Get<DBConnectionSettings>();

try
{
    var mongoClient = new MongoClient(dbConnectionSettings.ConnectionString);
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
