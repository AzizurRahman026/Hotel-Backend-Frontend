using Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSingleton<IHotelServices, HotelServices>();

var app = builder.Build();


app.MapControllers();

app.Run();
