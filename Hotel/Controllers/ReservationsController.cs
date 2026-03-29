using Hotel.Models;
using Hotel.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reservations = await _context.Reservations.ToListAsync();
            return Ok(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            //if (reservation.StartDate >= reservation.EndDate)
            //{
            //    return BadRequest("StartDate must be earlier than EndDate");
            //}

            //bool isConflict = await _context.Reservations
            //    .AnyAsync(r =>
            //        r.StartDate < reservation.EndDate &&
            //        r.EndDate > reservation.StartDate);

            //if (isConflict)
            //{
            //    return BadRequest("The date is already booked");
            //}

            //_context.Reservations.Add(reservation);
            //await _context.SaveChangesAsync();

            return Ok(reservation);
        }
    }
}