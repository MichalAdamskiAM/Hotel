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

        public async Task<ICollection<GettingRoomDto>> Search(ClaimsPrincipal user, RoomSearchingDTO search)
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

            search.AmenityIds = [.. search.AmenityIds.Where(ai =>
                dbContext.Amenities.Any(a => a.Id == ai))];

            return
            [
                .. dbContext.Rooms
                    .Include(r => r.RoomAmenities)
                        .ThenInclude(ra => ra.Amenity)
                    .Where(r => !unavailableRoomIds.Contains(r.Id))
                    .AsEnumerable()
                    .Select(r => new
                    {
                        Room = r,
                        Score = r.MatchScore(search)
                    })
                    .Where(x => x.Score > 0)
                    .OrderByDescending(x => x.Score)
                    .Select(x => new GettingRoomDto{
                        Id = x.Room.Id,
                        Number = x.Room.Number,
                        NumberOfPeople = x.Room.NumberOfPeople,
                        Area = x.Room.Area,
                        Price = x.Room.Price,
                        Description = x.Room.Description,
                        AmenityNames = [.. x.Room.RoomAmenities.Select(ra => ra.Amenity.Name)],
                        MatchScore = x.Score,
                        MissingAmenityNames = [..
                            x.Room.MissingAmenityIds(search).Select(ai =>
                            dbContext.Amenities.First(a => a.Id == ai).Name
                        )],
                        MissingKeywords = x.Room.MissingKeywords(search)
                    })
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