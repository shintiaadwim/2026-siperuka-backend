using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Models;

namespace Backend.Data.Configurations;
public class BookingHistoryConfiguration : IEntityTypeConfiguration<BookingHistory>
{
    public void Configure(EntityTypeBuilder<BookingHistory> builder)
    {
        // Primary Key
        builder.HasKey(bh => bh.Id);

        // Relasi BookingHistory dengan BookingStatus untuk OldStatus
        builder.HasOne(bh => bh.OldStatusNavigation)
            .WithMany(bs => bs.BookingHistoriesAsOldStatus)
            .HasForeignKey(bh => bh.OldStatus)
            .OnDelete(DeleteBehavior.Restrict);

        // Relasi BookingHistory dengan BookingStatus untuk NewStatus
        builder.HasOne(bh => bh.NewStatusNavigation)
            .WithMany(bs => bs.BookingHistoriesAsNewStatus)
            .HasForeignKey(bh => bh.NewStatus)
            .OnDelete(DeleteBehavior.Restrict);

        // Relasi User dengan BookingHistory (ChangedBy)
        builder.HasOne(bh => bh.ChangedByNavigation)
            .WithMany(u => u.BookingHistories)
            .HasForeignKey(bh => bh.ChangedBy)
            .OnDelete(DeleteBehavior.Restrict);

        // Relasi BookingHistory dengan Booking
        builder.HasOne(bh => bh.Booking)
            .WithMany(b => b.BookingHistories)
            .HasForeignKey(bh => bh.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        // Property Note
        builder.Property(bh => bh.Note)
            .HasMaxLength(500);
    }
}