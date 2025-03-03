using AutoMapper;
using EventsWebApplication.Core.Interfaces;
using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Application.Mapping
{
    // Application/Mapping/EventProfile.cs
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            // Create -> Entity
            CreateMap<EventCreateDto, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore());

            // Update -> Entity
            CreateMap<EventUpdateDto, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore());

            // Entity -> Response
            CreateMap<Event, EventReadDto>()
                .ForMember(dest => dest.CategoryReadDto, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.CurrentParticipants, opt => opt.MapFrom(src => src.Participants.Count));
        }
    }
}
