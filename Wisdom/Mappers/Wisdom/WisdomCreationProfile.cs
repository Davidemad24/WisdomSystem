using AutoMapper;
using Wisdom.DTOs.Wisdom;
using Wisdom.Entities;

namespace Wisdom.Mappers.Wisdom;

public class WisdomCreationProfile : Profile
{
    public WisdomCreationProfile()
    {
        CreateMap<WisdomCreationDto, Entities.Wisdom>()
            .ForMember(wisdom => wisdom.Id, 
                opt => opt.Ignore())
            .ForMember(wisdom => wisdom.User, opt => 
                opt.MapFrom(wisdomCreationDto => new User{Id = wisdomCreationDto.UserId}));
    }
}