using static FaintAlertAPI.DTOs.EmergencyContact.GetEmergencyContactDto;
using System.ComponentModel.DataAnnotations;
namespace FaintAlertAPI.DTOs.User;

public class CreateUserDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
    public required string Name { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
}
