using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel.Context;
using Hotel.DTOs;
using Hotel.Services;
using Hotel.Models;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly JwtService jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            this.context = context;
            this.jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return Unauthorized("There is no user with this email. Sign up first.");

            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (!isValid)
                return Unauthorized("Incorrect password. Try again.");

            var token = jwtService.GenerateToken(user);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser is not null)
                return BadRequest("User with this email already exists");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Ok("User created");
        }
    }

}