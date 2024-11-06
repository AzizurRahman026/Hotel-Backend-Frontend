using Data;
using IServices;
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
builder.Services.AddSingleton<IUserServices, UserServices>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();


app.MapControllers();

app.Run();
