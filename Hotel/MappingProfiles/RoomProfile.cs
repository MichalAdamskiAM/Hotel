using AutoMapper;
using Hotel.Models;
using Hotel.DTOs;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<CreatingRoomDto, Room>();
    }
}