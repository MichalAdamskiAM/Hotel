using AutoMapper;
using HotelBackend.DTOs;
using HotelBackend.Models;

namespace HotelBackend.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpdatingUserDTO, User>().ForAllMembers(member =>
                member.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}