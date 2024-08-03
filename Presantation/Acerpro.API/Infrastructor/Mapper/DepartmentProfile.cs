using Acerpro.Common.DTOs.Department;
using Acerpro.Model.Entities;
using AutoMapper;

namespace Acerpro.API.Infrastructor.Mapper
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentRequest>()
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<Department, DepartmentResponse>()
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
