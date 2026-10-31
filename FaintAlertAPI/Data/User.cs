using Microsoft.AspNetCore.Identity;

namespace FaintAlertAPI.Data;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public IList<EmergencyContact> EmergencyContacts { get; set; } = [];
}