using AutoMapper;
using Entites;
using Entites.Dtos.Requests;

namespace API.MappingProfiles;

public class RequestToDomain : Profile
{
    public RequestToDomain()
    {
        CreateMap<CreateDriverAchievementRequest, Achievement>()
            .ForMember(dest => dest.RaceWins, opt =>
                opt.MapFrom((src => src.Wins)))
            .ForMember(dest => dest.Status , opt => 
                opt.MapFrom(src => 1))
            .ForMember(dest => dest.CreatedAt , opt =>
                opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt =>
                opt.MapFrom(src => DateTime.UtcNow));
        
        
        CreateMap<UpdateDriverAchievementRequest, Achievement>()
            .ForMember(dest => dest.RaceWins, opt =>
                opt.MapFrom((src => src.Wins)))
            .ForMember(dest => dest.UpdatedAt, opt =>
                opt.MapFrom(src => DateTime.UtcNow));

        
    }
}