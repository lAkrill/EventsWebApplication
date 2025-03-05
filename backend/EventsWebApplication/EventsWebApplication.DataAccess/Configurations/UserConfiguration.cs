using EventsWebApplication.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsWebApplication.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            
            builder
                .Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            builder
                .Property(u => u.PasswordHash)
                .IsRequired();

            builder
                .Property(u => u.Birthday)
                .IsRequired();

            builder
                .Property(u => u.Role)
                .IsRequired();
        }
    }
}
