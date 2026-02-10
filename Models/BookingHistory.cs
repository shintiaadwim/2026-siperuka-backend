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
    
    public int ChangedBy { get; set; }
    public virtual User ChangedByNavigation { get; set; } = null!;
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    public string Note { get; set; } = string.Empty;   
}