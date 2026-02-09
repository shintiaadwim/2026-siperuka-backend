using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data.Seeders;

public static class BookingStatusSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookingStatus>().HasData(
            new BookingStatus
            {
                Id = 1,
                StatusBooking = "Pending"
            },
            new BookingStatus
            {
                Id = 2,
                StatusBooking = "Approved"
            },
            new BookingStatus
            {
                Id = 3,
                StatusBooking = "Rejected"
            },
            new BookingStatus
            {
                Id = 4,
                StatusBooking = "Cancelled"
            }
        );
    }
}
