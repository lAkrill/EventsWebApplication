using AutoMapper;
using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Application.Mapping
{
    public class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            CreateMap<Participant, ParticipantEventDto>()
                .ForMember(dest => dest.UserReadDto, opt=> opt.MapFrom(src=>src.User));

            CreateMap<Participant, ParticipantUserDto>()
                .ForMember(dest => dest.EventReadDto, opt => opt.MapFrom(src => src.Event));
        }
    }
}
