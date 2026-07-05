using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel.Context;
using Hotel.DTOs;
using Hotel.Services;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(AppDbContext dbContext, JwtService jwtService) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IAuthorizationService authService)
        {
            return NotFound();
            //to implement
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromServices] IAuthorizationService authService)
        {
            return NotFound();
            //to implement
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

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
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser is not null)
                return Conflict("User with this email already exists.");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }
    }
}