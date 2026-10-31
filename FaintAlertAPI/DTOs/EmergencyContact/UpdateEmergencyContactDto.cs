using System.ComponentModel.DataAnnotations;

namespace FaintAlertAPI.DTOs.EmergencyContact;

public class UpdateEmergencyContactDto : CreateEmergencyContactDto
{
    [Required]
    public required Guid Id { get; set; }
}
