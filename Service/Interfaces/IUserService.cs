using Model.Responses;

namespace Service.Interfaces;

public interface IUserService
{
    public Task<ICollection<UserResponse>> GetUsers();
    public Task<UserResponse> GetUser(Guid userId);
}
