using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FaintAlertAPI.Data
{
    public class FaintAlertDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public FaintAlertDbContext(DbContextOptions<FaintAlertDbContext> options) : base(options)
        {
        }
        public DbSet<EmergencyContact> EmergencyContacts => Set<EmergencyContact>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>()
                .HasMany(user => user.EmergencyContacts)
                .WithOne(contact => contact.User)
                .HasForeignKey(contact => contact.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
