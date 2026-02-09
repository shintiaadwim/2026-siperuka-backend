using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Models;

namespace Backend.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        // Primary key
        builder.HasKey(b => b.Id);

        // Required foreign keys
        builder.Property(b => b.RoomId).IsRequired();
        builder.Property(b => b.UserId).IsRequired();
        builder.Property(b => b.StatusId).IsRequired();

        // Required booking time fields
        builder.Property(b => b.Date).IsRequired();
        builder.Property(b => b.StartTime).IsRequired();
        builder.Property(b => b.EndTime).IsRequired();

        // Purpose length constraint
        builder.Property(b => b.Purpose)
            .IsRequired()
            .HasMaxLength(500);

        // Room relation
        builder.HasOne(b => b.Room)
            .WithMany(r => r.Bookings)
            .HasForeignKey(b => b.RoomId);

        // User relation
        builder.HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId);

        // Status relation
        builder.HasOne(b => b.Status)
            .WithMany(bs => bs.Bookings)
            .HasForeignKey(b => b.StatusId);
    }
}