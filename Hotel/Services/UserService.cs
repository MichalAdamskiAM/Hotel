using Hotel.Context;
using Hotel.DTOs;
using Hotel.Exceptions;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Hotel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Services
{
    public class UserService(AppDbContext dbContext, IAuthorizationService authService, JwtService jwtService)
    {
        public async Task<ICollection<User>> Get(ClaimsPrincipal user, int? id = null)
        {
            var privilegeRequirement = new PrivilegeRequirement("SeeAllUsers");

            if ((await authService.AuthorizeAsync(user, null, privilegeRequirement)).Succeeded)
            {
                return await dbContext.Users.Where(r => id == null || r.Id == id).ToListAsync();
            }

            throw new AccessDeniedException(privilegeRequirement);
        }

        public async Task<string> Login(LoginDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email)
                ?? throw new ArgumentException("There is no user with this email. Sign up first.");

            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (!isValid)
                throw new ArgumentException("Incorrect password. Try again.");

            var token = jwtService.GenerateToken(user);
            return await token;
        }

        public async Task<User> Add(RegisterDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser is not null)
                throw new ArgumentException("User with this email already exists.");

            var newUser = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            dbContext.Users.Add(newUser);
            await dbContext.SaveChangesAsync();
            return newUser;
        }
    }
}