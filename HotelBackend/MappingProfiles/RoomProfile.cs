using AutoMapper;
using Hotel.DTOs;
using Hotel.Models;

namespace Hotel.MappingProfiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<CreatingRoomDto, Room>();
        }
    }
}