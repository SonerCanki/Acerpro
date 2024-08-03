using Acerpro.Common.DTOs.Login;
using Acerpro.UI.Models.AccountViewModels;
using AutoMapper;

namespace Acerpro.Web.UI.Infrasructure.Mappers
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<LoginViewModel, LoginRequest>()
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));
                
        }
    }
}
