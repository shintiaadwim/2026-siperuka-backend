using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<BookingStatus> BookingStatuses { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingHistory> BookingHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Konfigurasi relasi BookingHistory dengan BookingStatus untuk OldStatus dan NewStatus
        modelBuilder.Entity<BookingHistory>()
            .HasOne(bh => bh.OldStatusNavigation)
            .WithMany(bs => bs.BookingHistoriesAsOldStatus)
            .HasForeignKey(bh => bh.OldStatus)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BookingHistory>()
            .HasOne(bh => bh.NewStatusNavigation)
            .WithMany(bs => bs.BookingHistoriesAsNewStatus)
            .HasForeignKey(bh => bh.NewStatus)
            .OnDelete(DeleteBehavior.Restrict);

        // Konfigurasi relasi User dengan BookingHistory
        modelBuilder.Entity<BookingHistory>()
            .HasOne(bh => bh.ChangedByNavigation)
            .WithMany(u => u.BookingHistories)
            .HasForeignKey(bh => bh.ChangedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}