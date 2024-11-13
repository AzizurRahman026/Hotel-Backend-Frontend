using System.Text;
using Data;
using IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Allow requests from Angular frontend (running on port 4200)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});*/

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

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
app.UseCors("AllowAll");

// app.UseCors("AllowSpecificOrigin");

// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
