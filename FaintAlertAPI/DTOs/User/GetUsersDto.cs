namespace FaintAlertAPI.DTOs.User;

public record GetUsersDto(
    Guid Id,
    string Name,
    string Email
);
