using System;
namespace Backend.Models;

public class BookingHistory
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public virtual Booking Booking { get; set; } = null!;

    public int OldStatus { get; set; }
    public virtual BookingStatus OldStatusNavigation { get; set; } = null!;
    public int NewStatus { get; set; }
    public virtual BookingStatus NewStatusNavigation { get; set; } = null!;

    // Field untuk mencatat semua jenis perubahan
    public string? ChangedField { get; set; } = string.Empty; // "Status", "Date", "StartTime", "EndTime", "Purpose", dll
    public string? OldValue { get; set; } = string.Empty; // Nilai lama
    public string? NewValue { get; set; } = string.Empty; // Nilai baru
    public string? EntityType { get; set; } = "Booking"; // "Booking" atau "Room"

    public int ChangedBy { get; set; }
    public virtual User ChangedByNavigation { get; set; } = null!;
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    public string Note { get; set; } = string.Empty;
}