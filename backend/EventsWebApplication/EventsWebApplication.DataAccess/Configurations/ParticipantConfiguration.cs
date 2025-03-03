using EventsWebApplication.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsWebApplication.DataAccess.Configurations
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder
                .HasKey(p => new { p.EventId, p.UserId });

            builder
                .HasOne(p => p.User)
                .WithMany(u => u.Participants)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(p => p.Event)
                .WithMany(u => u.Participants)
                .HasForeignKey(p => p.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(p => p.RegistrationDate)
                .IsRequired();
        }
    }
}
