using Hotel.Context;
using Hotel.DTOs;
using Hotel.Models;
using Hotel.Services;
using Hotel.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using Hotel.Authorization;

namespace Hotel.Services
{
    public class RoomService(AppDbContext dbContext, IMapper mapper, IAuthorizationService authService)
    {
        public async Task<ICollection<Room>> Get(ClaimsPrincipal user, int? id = null)
        {
            var privilegeRequirement = new PrivilegeRequirement("SeeAllRooms");

            if ((await authService.AuthorizeAsync(user, null, privilegeRequirement)).Succeeded)
            {
                return await dbContext.Rooms.Where(r => id == null || r.Id == id).ToListAsync();
            }
            throw new AccessDeniedException(privilegeRequirement);
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
            return newRoom;
        }
    }
}