using System.Text;
using Data;
using IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
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

// Add JWT Authentication configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["tokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Add authorization
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline...// Configure CORS first
app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
