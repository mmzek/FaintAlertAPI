namespace FaintAlertAPI.DTOs.EmergencyContact;

public record GetEmergencyContactsDto(
    Guid Id,
    string Name,
    string Email,
    string Phone,
    Guid UserId
);
