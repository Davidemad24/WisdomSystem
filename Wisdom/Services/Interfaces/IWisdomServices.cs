using Wisdom.DTOs;
using Wisdom.DTOs.Wisdom;

namespace Wisdom.Services.Interfaces;

public interface IWisdomServices
{
    Task<ICollection<WisdomDto>> GetUserWisdoms(int userId);
    Task<ServiceResult> Add(WisdomCreationDto wisdomCreationDto);
    Task<ServiceResult> Update(WisdomUpdatingDto wisdomUpdatingDto);
    Task<ServiceResult> Delete(int id);
}