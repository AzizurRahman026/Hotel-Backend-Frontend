using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entities;
using IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(User user)
        {
            var tokenKey = _config["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appsettings...");
            if (tokenKey.Length < 64)
            {
                throw new Exception("Your tokenKey needs to be longer...");
            }

            // key...
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            // payload { claims }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username)
            };

            // creds {signature}
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            // Generate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
