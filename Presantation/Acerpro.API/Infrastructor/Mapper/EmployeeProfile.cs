using Acerpro.Common.DTOs.Employee;
using Acerpro.Model.Entities;
using AutoMapper;

namespace Acerpro.API.Infrastructor.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, AddEmployeeRequest>()
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<Employee, UpdateEmployeeRequest>()
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Employee, EmployeeResponse>()
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
