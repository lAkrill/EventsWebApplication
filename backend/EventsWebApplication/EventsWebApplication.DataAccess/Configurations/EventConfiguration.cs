using EventsWebApplication.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsWebApplication.DataAccess.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(e => e.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder
                .Property(e => e.Date)
                .IsRequired();

            builder
                .Property(e => e.Location)
                .HasMaxLength(200)
                .IsRequired();  

            builder
                .Property(e => e.MaxParticipants)
                .IsRequired();

            builder
                .Property(e => e.ImagePath);

            builder
                .HasOne(e => e.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CategoryId);

            builder
                .HasIndex(e => e.Title)
                .IsUnique();
        }
    }
}
