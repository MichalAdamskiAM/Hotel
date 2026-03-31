using Hotel.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotel.Services
{
    public class JwtService
    {
        private readonly string? key;

        public JwtService(IConfiguration config)
        {
            key = config["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new Exception("JWT Key is not configured. Set Jwt:Key in configuration.");
            }
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            var bytesKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(bytesKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims, expires: DateTime.Now.AddHours(1), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}