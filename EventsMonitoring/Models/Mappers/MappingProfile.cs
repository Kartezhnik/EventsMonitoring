using EventsMonitoring.Models.Entities;
using AutoMapper;

namespace EventsMonitoring.Models.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
