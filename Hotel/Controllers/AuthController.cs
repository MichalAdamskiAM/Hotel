using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel.Context;
using Hotel.DTOs;
using Hotel.Services;
using BCrypt.Net;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly JwtService jwtService;

        public AuthController(AppDbContext context)
        {
            this.context = context;
            this.jwtService = new JwtService();
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
    }

}