using Hotel.DTOs;
using Hotel.Exceptions;
using Hotel.Models;
using Hotel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class UsersController(UserService userService) : ControllerBase
    {
        [Authorize, HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try { return Ok(await userService.Get(User)); }
            catch (AccessDeniedException) { return Forbid(); }
        }

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> Get(int id)
        {
            try { return Ok(await userService.Get(User, id)); }
            catch (AccessDeniedException) { return Forbid(); }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var token = await userService.Login(dto);
                return Ok(new { token });
            }
            catch (ArgumentException e) { return Unauthorized(e.Message); }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                var createdUser = await userService.Add(dto);
                return CreatedAtAction(
                    nameof(Get), new { id = createdUser.Id }, createdUser);
            }
            catch (ArgumentException e) { return Conflict(e.Message); }
        }

        [HttpPatch("{id}"), Authorize]
        public async Task<IActionResult> Update(int id, UpdatingUserDTO dto)
        {
            User updatedUser;

            try { updatedUser = await userService.Update(User, id, dto); }
            catch (ArgumentException e) { return NotFound(e.Message); }
            catch (AccessDeniedException) { return Forbid(); }

            return Ok(updatedUser);
        }
    }
}