using AutoMapper;
using HRSystem.Application.Dtos;
using HRSystem.Core.Entities;
namespace TheCityGaurdsApp.Common.Helpers
{
    //Help in runtime mapping
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DepartmentDto, Department>().ReverseMap();
        }
    }
}
