using Hotel.Context;
using Hotel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotel.Services
{
    public class JwtService
    {
        private readonly string key;
        private readonly AppDbContext dbContext;

        public JwtService(IConfiguration config, AppDbContext dbContext)
        {
            this.dbContext = dbContext;

            if (string.IsNullOrWhiteSpace(config["Jwt:Key"]))
                throw new ArgumentException("JWT Key is not configured.");
            key = config["Jwt:Key"]!;
        }

        public async Task<string> GenerateToken(User user)
        {
            var role = await dbContext.Roles
                .Include(r => r.RolePrivileges)
                .ThenInclude(rp => rp.Privilege)
                .FirstAsync(r => r.Id == user.RoleId);

            var privileges = role.RolePrivileges
                .Select(rp => rp.Privilege.Name)
                .ToList();

            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, user.Id.ToString()),
                new (ClaimTypes.Role, user.RoleId.ToString())
            };

            foreach (var privilege in privileges)
            {
                claims.Add(new("Privilege", privilege));
            }

            var bytesKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(bytesKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                //claims: claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);
                claims: claims, expires: DateTime.Now.AddHours(3), signingCredentials: creds); //for easier testing

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}