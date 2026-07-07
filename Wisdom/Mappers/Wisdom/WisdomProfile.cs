using AutoMapper;
using Wisdom.DTOs.Wisdom;

namespace Wisdom.Mappers.Wisdom;

public class WisdomProfile : Profile
{
    public WisdomProfile()
    {
        CreateMap<Entities.Wisdom, WisdomDto>();
    }
}