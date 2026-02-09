using System;
using System.Collections.Generic;
namespace Backend.Models;

public class Booking
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public int UserId { get; set; }
    public int StatusId { get; set; }

    // Booking Details
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public string Purpose { get; set; } = string.Empty;

    // Audit Fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } // Soft delete

    // Navigation Properties
    public virtual Room Room { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public virtual BookingStatus Status { get; set; } = null!;
    public virtual ICollection<BookingHistory> BookingHistories { get; set; } = new List<BookingHistory>();
}