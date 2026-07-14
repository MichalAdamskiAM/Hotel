using AutoMapper;
using HotelBackend.DTOs;
using HotelBackend.Models;

namespace HotelBackend.MappingProfiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<CreatingRoomDto, Room>();
        }
    }
}