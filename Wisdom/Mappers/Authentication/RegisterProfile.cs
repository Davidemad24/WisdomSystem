using AutoMapper;
using Wisdom.Entities;
using Wisdom.DTOs.Authentication;

namespace Wisdom.Mappers.Authentication;

public class RegisterProfile : Profile
{
    public RegisterProfile()
    {
        CreateMap<RegisterDto, User>()
            .ForMember(user => user.Id,
                opt => opt.Ignore())
            .ForMember(user => user.RefreshTokens,
                opt => opt.Ignore())
            .ForMember(user => user.Wisdoms,
                opt => opt.Ignore());
    }
}