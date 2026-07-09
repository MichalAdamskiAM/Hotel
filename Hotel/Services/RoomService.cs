using AutoMapper;
using Hotel.Authorization;
using Hotel.Context;
using Hotel.DTOs;
using Hotel.Exceptions;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hotel.Services
{
    public class RoomService(AppDbContext dbContext, IMapper mapper, IAuthorizationService authService)
    {
        public async Task<ICollection<GettingRoomDto>> Get(ClaimsPrincipal user, int? id = null)
        {
            var privilegeRequirement = new PrivilegeRequirement("SeeAllRooms");

            if ((await authService.AuthorizeAsync(user, null, privilegeRequirement)).Succeeded)
            {
                return await dbContext.Rooms
                    .Include(r => r.RoomAmenities)
                        .ThenInclude(ra => ra.Amenity)
                    .Where(r => id == null || r.Id == id)
                    .Select(r => new GettingRoomDto
                    {
                        Id = r.Id,
                        Number = r.Number,
                        NumberOfPeople = r.NumberOfPeople,
                        Area = r.Area,
                        Price = r.Price,
                        Description = r.Description,
                        AmenityNames = r.RoomAmenities
                            .Select(ra => ra.Amenity.Name)
                            .ToList()
                    })
                    .ToListAsync();
            }
            throw new AccessDeniedException(privilegeRequirement);
        }

        public async Task<ICollection<Room>> GetAvailable(ClaimsPrincipal user, RoomSearchingDTO search)
        {
            ArgumentNullException.ThrowIfNull(search);

            var privilegeRequirement = new PrivilegeRequirement("SeeAllRooms");

            if (!(await authService.AuthorizeAsync(user, null, privilegeRequirement)).Succeeded)
            {
                throw new AccessDeniedException(privilegeRequirement);
            }

            var unavailableRoomIds = new List<int>();

            if (search.StartDate is not null)
            {
                unavailableRoomIds.AddRange([.. dbContext.RoomReservations
                    .Where(rr => search.StartDate >= rr.Reservation.StartDate && search.StartDate < rr.Reservation.EndDate)
                    .Select(rr => rr.RoomId)]);
            }
            if (search.EndDate is not null)
            {
                unavailableRoomIds.AddRange([.. dbContext.RoomReservations
                    .Where(rr => search.EndDate > rr.Reservation.StartDate && search.EndDate <= rr.Reservation.EndDate)
                    .Select(rr => rr.RoomId)]);
            }

            return [.. dbContext.Rooms
                .Where(r => !unavailableRoomIds.Contains(r.Id))
                .Where(r => r.HasAmenities(search.AmenityIds))
                //price, area, people, etc.
            ];
        }

        public async Task<Room> Add(ClaimsPrincipal user, CreatingRoomDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var privilegeRequirement = new PrivilegeRequirement("ManageAllRooms");
            if (!(await authService.AuthorizeAsync(user, null, privilegeRequirement)).Succeeded)
            {
                throw new AccessDeniedException(privilegeRequirement);
            }

            if (dbContext.Rooms.Any(r => r.Number == dto.Number))
            {
                throw new ArgumentException("Room with this number already exists.");
            }

            var newRoom = mapper.Map<Room>(dto);
            dbContext.Rooms.Add(newRoom);

            await dbContext.SaveChangesAsync();

            foreach (var amenityId in dto.AmenityIds)
            {
                dbContext.RoomAmenities.Add(new RoomAmenity { RoomId = newRoom.Id, AmenityId = amenityId });
            }

            await dbContext.SaveChangesAsync();
            return newRoom;
        }
    }
}