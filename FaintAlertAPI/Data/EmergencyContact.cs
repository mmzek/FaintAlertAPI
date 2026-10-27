namespace FaintAlertAPI.Data
{
    public class EmergencyContact
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public User? User { get; set; }
    }
}
