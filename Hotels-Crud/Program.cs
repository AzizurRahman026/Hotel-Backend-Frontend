using Data;
using MongoDB.Driver;

using Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var connectionUri = "mongodb://localhost:27017"; // Adjust if needed
    var databaseName = "Hotel";
    return new MongoDbContext(connectionUri, databaseName);
});

builder.Services.AddSingleton<IHotelServices, HotelServices>();

var app = builder.Build();


app.MapControllers();

app.Run();
