using Hotel.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hotel.DTOs;
using Hotel.Services;
using Hotel.Exceptions;
using AutoMapper;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public RoomsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromServices] IAuthorizationService authService,
            [FromServices] IMapper mapper)
        {
            var roomService = new RoomService(dbContext, mapper, authService, User);

            try
            {
                return Ok(await roomService.GetAll());
            }
            catch (AccessDeniedException e)
            {
                return Forbid(e.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get([FromServices] IAuthorizationService authService,
            [FromServices] IMapper mapper)
        {
            return NotFound();
            // to implement
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromServices] IAuthorizationService authService,
            [FromBody] CreatingRoomDto dto, [FromServices] IMapper mapper)
        {
            var roomService = new RoomService(dbContext, mapper, authService, User);

            try
            {
                return CreatedAtAction(
                    nameof(Get), new { number = dto.Number }, await roomService.Add(dto));
            }
            catch (AccessDeniedException e)
            {
                return Forbid(e.Message);
            }
            catch (ArgumentException e)
            {
                return Conflict(e.Message);
            }
        }
    }
}