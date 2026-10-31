namespace FaintAlertAPI.Data;
using System.ComponentModel.DataAnnotations;


public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public IList<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();
}