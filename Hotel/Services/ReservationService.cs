using Hotel.Context;
using Hotel.DTOs;
using Hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Services
{
    public class ReservationService
    {
        private readonly AppDbContext dbContext;

        public ReservationService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Adds a new reservation to the database if the specified user and rooms exist in the database
        /// and if there is no conflict with existing reservations.
        /// </summary>
        /// <param name="dto">
        /// A <see cref="CreatingReservationDto"/> containing the reservation data:
        /// start and end dates, user ID, and the IDs of the rooms to reserve.
        /// </param>
        /// <returns>
        /// A <see cref="Reservation"/> entity representing the newly created reservation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="dto"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item>The specified user does not exist.</item>
        /// <item>One or more of the requested rooms do not exist.</item>
        /// <item>One or more of the requested rooms are already booked for the specified date range.</item>
        /// </list>
        /// </exception>
        /// <remarks>
        /// This method performs the following steps:
        /// <list type="number">
        /// <item>Checks if <paramref name="dto"/> is null and throws <see cref="ArgumentNullException"/> if so.</item>
        /// <item>Verifies that the user specified by <see cref="CreatingReservationDto.UserId"/> exists.</item>
        /// <item>Verifies that all room ids specified in <see cref="CreatingReservationDto.RoomIds"/> exist in the database.</item>
        /// <item>Checks for conflicts with existing reservations for the specified rooms and date range.</item>
        /// <item>If all checks pass, creates a new <see cref="Reservation"/> entity, sets its <see cref="Reservation.UserId"/> 
        /// and <see cref="Reservation.StatusId"/>, adds corresponding <see cref="RoomReservation"/> entries for each room, 
        /// and saves the changes to the database.</item>
        /// </list>
        /// </remarks>
        public async Task<Reservation> Add(CreatingReservationDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            bool userExists = await dbContext.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!userExists)
                throw new InvalidOperationException($"User with id {dto.UserId} doesn't exist.");

            var existingRooms = await dbContext.Rooms
                .Where(r => dto.RoomIds.Contains(r.Id))
                .Select(r => r.Id)
                .ToListAsync();

            var missingRooms = dto.RoomIds.Except(existingRooms).ToList();

            if (missingRooms.Any())
                throw new InvalidOperationException($"Rooms with id {string.Join(", ", missingRooms)} don't exist.");

            bool isConflict = await dbContext.Reservations
                .Where(r => r.RoomReservations.Any(rr => dto.RoomIds.Contains(rr.RoomId)))
                .AnyAsync(r => r.StartDate < dto.EndDate && r.EndDate > dto.StartDate);

            if (isConflict)
                throw new InvalidOperationException("One or more rooms are already booked for the selected dates.");

            var newReservation = new Reservation
            {
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                UserId = dto.UserId,
                StatusId = 1,
                RoomReservations = dto.RoomIds.Select(ri => new RoomReservation
                {
                    RoomId = ri
                }).ToList()
            };

            dbContext.Reservations.Add(newReservation);
            await dbContext.SaveChangesAsync();
            return newReservation;
        }
    }
}