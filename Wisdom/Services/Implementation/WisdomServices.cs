using AutoMapper;
using Wisdom.DTOs;
using Wisdom.DTOs.Wisdom;
using Wisdom.Repositories.Interfaces;
using Wisdom.Services.Interfaces;

namespace Wisdom.Services.Implementation;

public class WisdomServices : IWisdomServices
{
    // Attributes
    private readonly IWisdomRepo _wisdomRepo;
    private readonly IUserRepo _userRepo;
    private readonly IMapper _mapper;
    
    // Constructor
    public WisdomServices(IWisdomRepo wisdomRepo, IUserRepo userRepo, IMapper mapper)
    {
        _wisdomRepo = wisdomRepo;
        _userRepo = userRepo;
        _mapper = mapper;
    }
    
    // Methods
    public async Task<ICollection<WisdomDto>> GetUserWisdoms(int userId)
    {
        // Get user wisdoms
        var wisdoms = await _wisdomRepo.GetUserWisdoms(userId);
        
        // Map to DTOs
        return _mapper.Map<ICollection<WisdomDto>>(wisdoms);
    }

    public async Task<ServiceResult> Add(WisdomCreationDto wisdomCreationDto)
    {
        // Map to entity
        var wisdom = _mapper.Map<Entities.Wisdom>(wisdomCreationDto);
        
        // Check user existence
        if (!await _userRepo.IsExistById(wisdom.UserId)) 
            return new ServiceResult{ Message = "User not exist.", StatusCode = 404 };
        
        // Check duplication
        if (await _wisdomRepo.CheckDuplication(wisdom)) 
            return new ServiceResult{ Message = "Error: wisdom duplicated.", StatusCode = 406 };
        
        // Add wisdom
        return (await _wisdomRepo.Add(wisdom)) 
            ? new ServiceResult{ Message = "Wisdom add successfully.", StatusCode = 200 }
            : new ServiceResult{ Message = "Error: something went wrong, try again later.", StatusCode = 500 };
    }

    public async Task<ServiceResult> Update(WisdomUpdatingDto wisdomUpdatingDto)
    {
        // Check wisdom existence
        var currentWisdom = await _wisdomRepo.FindById(wisdomUpdatingDto.Id);
        if (currentWisdom is null) return new ServiceResult{ Message = "Wisdom not exist.", StatusCode = 404 };

        // Replace current wisdom content by new content
        currentWisdom.Content = wisdomUpdatingDto.Content;
        
        // Check owner
        if (currentWisdom.UserId != wisdomUpdatingDto.UserId)
            return new ServiceResult { Message = "Error: user not own this wisdom.", StatusCode = 402 };

        // Check duplication
        if (await _wisdomRepo.CheckDuplication(currentWisdom))
            return new ServiceResult{ Message = "Error: wisdom duplicated.", StatusCode = 406 };

        // Update wisdom
        return (await _wisdomRepo.Update(currentWisdom))
            ? new ServiceResult{ Message = "Wisdom updated successfully.", StatusCode = 200 }
            : new ServiceResult{ Message = "Error: something went wrong, try again later.", StatusCode = 500 };
    }

    public async Task<ServiceResult> Delete(int id)
    {
        // Check wisdom existence
        var wisdom = await _wisdomRepo.FindById(id);
        if (wisdom is null) return new ServiceResult{ Message = "Wisdom not exist.", StatusCode = 404 };
        
        // Delete wisdom
        return (await _wisdomRepo.Delete(wisdom))
            ? new ServiceResult{ Message = "Wisdom deleted successfully.", StatusCode = 200 }
            : new ServiceResult{ Message = "Error: something went wrong, try again later.", StatusCode = 500 };
    }
}