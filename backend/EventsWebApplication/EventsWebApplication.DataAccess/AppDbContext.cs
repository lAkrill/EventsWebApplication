using EventsWebApplication.Core.Models;
using Microsoft.EntityFrameworkCore;
using EventsWebApplication.DataAccess.Algorithms;

namespace EventsWebApplication.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Participant> Participants { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@admin.com",
                    PasswordHash = new PasswordHasher().Hash("admin"),
                    Birthday = DateOnly.FromDateTime(DateTime.UtcNow),
                    Role = UserRole.Admin
                }
            );
        }
    }
}