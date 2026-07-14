using AutoMapper;
using Hotel.DTOs;
using Hotel.Models;

namespace Hotel.MappingProfiles
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