using AutoMapper;
using Model;
using Model.Responses;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<UserResponse>> GetUsers()
    {
        return await _userRepository.GetAll().Select(u => _mapper.Map<UserResponse>(u)).ToArrayAsync();
    }

    public async Task<UserResponse> GetUser(Guid userId)
    {
        User user = await _userRepository.GetById(userId) ?? throw new NotFoundException("user");
        return _mapper.Map<UserResponse>(user);
    }
}
