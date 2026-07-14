using Hotel.DTOs;
using Hotel.Exceptions;
using Hotel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class RoomsController(RoomService roomService) : ControllerBase
    {
        [HttpGet, Authorize]
        public async Task<IActionResult> GetAll()
        {
            try { return Ok(await roomService.Get(User)); }
            catch (AccessDeniedException) { return Forbid(); }
        }

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> Get(int id)
        {
            try { return Ok(await roomService.Get(User, id)); }
            catch (AccessDeniedException) { return Forbid(); }
        }

        [HttpGet("search"), Authorize]
        public async Task<IActionResult> Search([FromQuery] RoomSearchingDTO search)
        {
            if (search.StartDate is not null && search.EndDate is not null && search.StartDate >= search.EndDate)
                return UnprocessableEntity("StartDate must be earlier than EndDate");

            try { return Ok(await roomService.Search(User, search)); }
            catch (AccessDeniedException) { return Forbid(); }
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create([FromBody] CreatingRoomDto dto)
        {
            try
            {
                var createdRoom = await roomService.Add(User, dto);
                return CreatedAtAction(
                    nameof(Get), new { id = createdRoom.Id }, createdRoom);
            }
            catch (AccessDeniedException) { return Forbid(); }
            catch (ArgumentException e) { return Conflict(e.Message); }
        }
    }
}