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

namespace Hotel.Services
{
    public class RoomService(AppDbContext dbContext, IMapper mapper, IAuthorizationService authService, ClaimsPrincipal user)
    {
        public async Task<ICollection> GetAll()
        {
            var privilegeRequirement = new PrivilegeRequirement("SeeAllRooms");

            if ((await authService.AuthorizeAsync(user, null, privilegeRequirement)).Succeeded)
            {
                return await dbContext.Rooms.ToListAsync();
            }
            throw new AccessDeniedException(privilegeRequirement);
        }

        public async Task<Room> Add(CreatingRoomDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

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