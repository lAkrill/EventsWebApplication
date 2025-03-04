using AutoMapper;
using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Application.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventCreateDto, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Participants, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore());

            CreateMap<EventUpdateDto, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Participants, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore());

            CreateMap<Event, EventReadDto>()
                .ForMember(dest => dest.CategoryReadDto, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.CurrentParticipants, opt => opt.MapFrom(src => src.Participants.Count));

            CreateMap<Event, EventParticipantDto>()
                .ForMember(dest => dest.ParticipantEventDtos, opt => opt.MapFrom(src => src.Participants));
        }
    }
}
