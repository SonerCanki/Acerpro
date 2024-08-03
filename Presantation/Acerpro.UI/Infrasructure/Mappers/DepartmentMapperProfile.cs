using Acerpro.Common.DTOs.Department;
using Acerpro.UI.Models.DepartmentViewModels;
using AutoMapper;

namespace Acerpro.UI.Infrasructure.Mappers
{
    public class DepartmentMapperProfile : Profile
    {
        public DepartmentMapperProfile()
        {
            CreateMap<AddDepartmentViewModel, DepartmentRequest>()
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<DepartmentViewModel, DepartmentRequest>()
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<DepartmentViewModel, DepartmentResponse>()
                .ReverseMap()
                .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
