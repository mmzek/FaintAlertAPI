using System.ComponentModel.DataAnnotations;
namespace FaintAlertAPI.DTOs.User;

public class UpdateUserDto : CreateUserDto
{
    [Required]
    public required Guid Id { get; set; }
}