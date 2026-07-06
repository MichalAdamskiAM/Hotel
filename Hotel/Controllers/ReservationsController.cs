using Hotel.DTOs;
using Hotel.Exceptions;
using Hotel.Models;
using Hotel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ReservationsController(ReservationService reservationService) : ControllerBase
    {
        [HttpGet, Authorize]
        public async Task<IActionResult> GetAll()
        {
            try { return Ok(await reservationService.Get(User)); }
            catch (AccessDeniedException) { return Forbid(); }
        }

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> Get(int id)
        {
            try { return Ok(await reservationService.Get(User, id)); }
            catch (AccessDeniedException) { return Forbid(); }
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(CreatingReservationDto dto)
        {
            if (dto.StartDate >= dto.EndDate)
                return UnprocessableEntity("StartDate must be earlier than EndDate");

            Reservation createdReservation;

            try { createdReservation = await reservationService.Add(User, dto); }
            catch (InvalidOperationException e) { return UnprocessableEntity(e.Message); }
            catch (AccessDeniedException) { return Forbid(); }

            return CreatedAtAction(nameof(Get), new { id = createdReservation.Id }, createdReservation);
        }
    }
}