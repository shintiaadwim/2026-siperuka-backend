using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Models;

namespace Backend.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Primary key
        builder.HasKey(u => u.Id);

        // Required user name with max length
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Required email with max length
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        // Required role with max length
        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(50);
    }
}