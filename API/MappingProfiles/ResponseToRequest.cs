using AutoMapper;
using Entites;
using Entites.Dtos.Responses;

namespace API.MappingProfiles;

public class DomainToResponse : Profile
{
    public DomainToResponse()
    {
        CreateMap<Achievement, DriverAchievementResponse>()
            .ForMember(dest => dest.Wins , opt =>
                opt.MapFrom(src => src.RaceWins));

        CreateMap<Driver, GetDriverResponse>()
            .ForMember(d => d.DriverId, opt =>
                opt.MapFrom(src => src.Id))
            .ForMember(d => d.FullName, opt =>
                opt.MapFrom(src => src.FirstName + " " + src.LastName));
    }
}