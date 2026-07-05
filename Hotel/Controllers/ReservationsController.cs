using Hotel.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Hotel.DTOs;
using Hotel.Services;
using Hotel.Models;
using Hotel.Exceptions;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController(ReservationService reservationService) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try { return Ok(await reservationService.Get(User)); }
            catch (AccessDeniedException) { return Forbid(); }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            try { return Ok(await reservationService.Get(User, id)); }
            catch (AccessDeniedException) { return Forbid(); }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromServices] IAuthorizationService authService, [FromBody] CreatingReservationDto dto)
        {
            if (dto.StartDate >= dto.EndDate)
                return BadRequest("StartDate must be earlier than EndDate");

            Reservation createdReservation;

            try
            {
                createdReservation = await reservationService.Add(User, dto);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (AccessDeniedException)
            {
                return Forbid();
            }

            return CreatedAtAction(nameof(Get), new { id = createdReservation.Id }, createdReservation);
        }
    }
}