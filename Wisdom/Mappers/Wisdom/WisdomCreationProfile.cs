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
            .ForMember(wisdom => wisdom.CreatedOn, opt => 
                opt.MapFrom(wisdomCreationDto => DateTime.UtcNow));
    }
}