using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Data.Configurations;

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

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new BookingConfiguration());
        modelBuilder.ApplyConfiguration(new BookingStatusConfiguration());
        modelBuilder.ApplyConfiguration(new BookingHistoryConfiguration());
    }
}