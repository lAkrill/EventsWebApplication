using AutoMapper;
using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Application.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<CategoryCreateDto, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Events, opt => opt.Ignore());

            CreateMap<CategoryUpdateDto, Category>()
                .ForMember(dest => dest.Events, opt => opt.Ignore());

            CreateMap<Category, CategoryReadDto>();
        }
    }
}
