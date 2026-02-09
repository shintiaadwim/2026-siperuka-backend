using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Models;

namespace Backend.Data.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        // Primary key
        builder.HasKey(r => r.Id);

        // Required room code with max length
        builder.Property(r => r.RoomCode)
            .IsRequired()
            .HasMaxLength(50);

        // Required room name with max length
        builder.Property(r => r.RoomName)
            .IsRequired()
            .HasMaxLength(100);

        // Required capacity
        builder.Property(r => r.Capacity)
            .IsRequired();

        // Required location with max length
        builder.Property(r => r.Location)
            .IsRequired()
            .HasMaxLength(100);

        // Required room status with max length
        builder.Property(r => r.RoomStatus)
            .IsRequired()
            .HasMaxLength(50);
    }
}
