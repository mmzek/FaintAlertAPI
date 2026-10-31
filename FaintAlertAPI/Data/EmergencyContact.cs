using System.ComponentModel.DataAnnotations;
namespace FaintAlertAPI.Data;

public class EmergencyContact
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public User? User { get; set; }
}
