using AutoMapper;
using Wisdom.DTOs.Authentication;
using Wisdom.Entities;

namespace Wisdom.Mappers.Authentication;

public class AuthenticationProfile : Profile
{
    public AuthenticationProfile()
    {
        CreateMap<User, AuthenticationDto>()
            .ForMember(authenticationDto => authenticationDto.IsAuthenticated,
                opt => opt.Ignore())
            .ForMember(authenticationDto => authenticationDto.Message,
                opt => opt.Ignore())
            .ForMember(authenticationDto => authenticationDto.Token,
                opt => opt.Ignore())
            .ForMember(authenticationDto => authenticationDto.RefreshToken,
                opt => opt.Ignore())
            .ForMember(authenticationDto => authenticationDto.ExpiresOn,
                opt => opt.Ignore());
    }
}