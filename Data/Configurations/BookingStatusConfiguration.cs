using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Models;

namespace Backend.Data.Configurations;
public class BookingStatusConfiguration : IEntityTypeConfiguration<BookingStatus>
{
    public void Configure(EntityTypeBuilder<BookingStatus> builder)
    {
        // Primary Key
        builder.HasKey(bs => bs.Id);

        // Property StatusBooking
        builder.Property(bs => bs.StatusBooking)
            .IsRequired()
            .HasMaxLength(50);
    }
}