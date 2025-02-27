using Azure;
using EventsWebApplication.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class AppDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Participant> Participants { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>(e =>
        {
            e.HasKey(x => x.Id); 

            e.Property(x => x.Title)
                .HasMaxLength(Event.MAX_TITLE_LENGTH)
                .IsRequired();

            e.Property(x => x.Description)
                .IsRequired();

            e.Property(x => x.Date)
                .IsRequired();

            e.Property(x => x.Location)
                .IsRequired();

            e.HasOne<Category>()
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        

        modelBuilder.Entity<User>(u =>
        {
            u.HasKey(x => x.Id);

            u.Property(x => x.PasswordHash)
                .IsRequired();

            u.Property(x => x.Email)
                .IsRequired();

            u.Property(x => x.FirstName)
                .IsRequired();

            u.Property(x => x.LastName)
                .IsRequired();
        });

        modelBuilder.Entity<Participant>(p =>
        {
            p.HasKey(x => x.Id);

            p.HasIndex(x => new { x.UserId, x.EventId })
                .IsUnique();

            p.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            p.HasOne<Event>()
                .WithMany(x => x.Participants)
                .HasForeignKey(x => x.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Category>(c =>
        {
            c.HasKey(x => x.Id);
            c.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);
        });
    }
}