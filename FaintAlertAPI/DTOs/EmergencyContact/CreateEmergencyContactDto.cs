using System.ComponentModel.DataAnnotations;

namespace FaintAlertAPI.DTOs.EmergencyContact;

public class CreateEmergencyContactDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
    public required string Name { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public required string Email { get; set; }
    [Required]
    public required string Phone { get; set; }
    [Required]
    public required Guid UserId { get; set; }
}
