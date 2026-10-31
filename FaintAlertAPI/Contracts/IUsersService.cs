using FaintAlertAPI.DTOs.User;
using FaintAlertAPI.Results;

namespace FaintAlertAPI.Contracts
{
    public interface IUsersService
    {
        Task<Result<GetUserDto>> CreateUserAsync(CreateUserDto userDto);
        Task<Result> DeleteUserAsync(Guid id);
        Task<Result<IEnumerable<GetUsersDto>>> GetAllUsersAsync();
        Task<Result<GetUserDto>> GetUserByIdAsync(Guid id);
        Task<Result> UpdateUserAsync(Guid id, UpdateUserDto userDto);
    }
}