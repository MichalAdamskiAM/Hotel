using Hotel.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Hotel.DTOs;
using Hotel.Services;
using Hotel.Models;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public ReservationsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromServices] IAuthorizationService authService)
        {
            if ((await authService.AuthorizeAsync(User, null,
                new PrivilegeRequirement("SeeAllReservations"))).Succeeded)
            {
                return Ok(await dbContext.Reservations.ToListAsync());
            }

            if ((await authService.AuthorizeAsync(User, null,
                new PrivilegeRequirement("SeeOwnReservations"))).Succeeded)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.Name)!.Value);

                return Ok(await dbContext.Reservations
                    .Where(r => r.UserId == userId)
                    .ToListAsync());
            }

            return Forbid();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromServices] IAuthorizationService authService)
        {
            return NotFound();
            //to implement
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromServices] IAuthorizationService authService, [FromBody] CreatingReservationDto dto)
        {
            var reservationService = new ReservationService(dbContext);

            if (dto.StartDate >= dto.EndDate)
                return BadRequest("StartDate must be earlier than EndDate");

            if (
                !(await authService.AuthorizeAsync(User, null,
                    new PrivilegeRequirement("ManageAllReservationsDates"))).Succeeded ||
                !(await authService.AuthorizeAsync(User, null,
                    new PrivilegeRequirement("ManageAllReservationsRooms"))).Succeeded
            )
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.Name)!.Value);
                if (
                    userId != dto.UserId ||
                    !(await authService.AuthorizeAsync(User, null,
                        new PrivilegeRequirement("ManageOwnReservationsDates"))).Succeeded ||
                    !(await authService.AuthorizeAsync(User, null,
                        new PrivilegeRequirement("ManageOwnReservationsRooms"))).Succeeded
                )
                {
                    return Forbid();
                }
            }

            Reservation createdReservation;

            try
            {
                createdReservation = await reservationService.Add(dto);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction(nameof(Get), new { id = createdReservation.Id }, createdReservation);
        }
    }
}