namespace FaintAlertAPI.DTOs.User;

public record GetUserDto(
    Guid Id,
    string Name,
    string Email,
    string Password
);
