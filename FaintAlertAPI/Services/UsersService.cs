using AutoMapper;
using AutoMapper.QueryableExtensions;
using FaintAlertAPI.Contracts;
using FaintAlertAPI.Data;
using FaintAlertAPI.DTOs.User;
using FaintAlertAPI.Results;
using Microsoft.EntityFrameworkCore;
namespace FaintAlertAPI.Services;

public class UsersService(FaintAlertDbContext context, IMapper mapper) : IUsersService
{
    public async Task<Result<IEnumerable<GetUsersDto>>> GetAllUsersAsync()
    {
        var users = await context.Users
            .ProjectTo<GetUsersDto>(mapper.ConfigurationProvider)
            .ToListAsync();
        return Result<IEnumerable<GetUsersDto>>.Success(users);
    }

    public async Task<Result<GetUserDto>> GetUserByIdAsync(Guid id)
    {
        var user = await context.Users
            .Where(u => u.Id == id)
            .ProjectTo<GetUserDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        return user is null
            ? Result<GetUserDto>.Failure(new Error("NotFoundError", "User not found."))
            : Result<GetUserDto>.Success(user);
    }

    public async Task<Result> UpdateUserAsync(Guid id, UpdateUserDto userDto)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return Result.Failure(new Error("NotFoundError", "User not found."));
        }

        mapper.Map(userDto, user);
        await context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<GetUserDto>> CreateUserAsync(CreateUserDto userDto)
    {
        var user = mapper.Map<User>(userDto);
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return Result<GetUserDto>.Success(mapper.Map<GetUserDto>(user));
    }

    public async Task<Result> DeleteUserAsync(Guid id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return Result.Failure(new Error("NotFoundError", "User not found."));
        }
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return Result.Success();
    }
}
