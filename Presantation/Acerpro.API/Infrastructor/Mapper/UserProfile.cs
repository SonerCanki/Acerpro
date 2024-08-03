using Acerpro.Common.DTOs.User;
using Acerpro.Model.Entities;
using AutoMapper;

namespace Acerpro.API.Infrastructor.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserRequest>()
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.FullName,
                                   opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
