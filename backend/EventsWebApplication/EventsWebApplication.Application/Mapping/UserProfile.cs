using AutoMapper;
using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>();

            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.Participants, opt => opt.Ignore());

            CreateMap<UserRegisterDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Participants, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<User, UserParticipantDto>()
                .ForMember(dest => dest.ParticipantUserDtos, opt => opt.MapFrom(src => src.Participants));
        }
    }
}
