namespace FaintAlertAPI.DTOs.EmergencyContact;

public record GetEmergencyContactDto(
    Guid Id,
    string Name,
    string Email,
    string Phone,
    string User
);
