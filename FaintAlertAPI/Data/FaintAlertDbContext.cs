using Microsoft.EntityFrameworkCore;

namespace FaintAlertAPI.Data
{
    public class FaintAlertDbContext(DbContextOptions<FaintAlertDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
    }
}
