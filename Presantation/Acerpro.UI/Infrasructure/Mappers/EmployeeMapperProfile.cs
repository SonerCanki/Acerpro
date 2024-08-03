using Acerpro.Common.DTOs.Employee;
using Acerpro.UI.Models.EmployeeViewModels;
using AutoMapper;

namespace Acerpro.UI.Infrasructure.Mappers
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<AddEmployeeViewModel, AddEmployeeRequest>()
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<UpdateEmployeeViewModel, UpdateEmployeeRequest>()
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<EmployeeViewModel, EmployeeResponse>()
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<UpdateEmployeeViewModel, EmployeeResponse>()
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
