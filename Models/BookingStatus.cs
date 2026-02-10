using System.Collections.Generic;
namespace Backend.Models;

public class BookingStatus
{
    public int Id { get; set; }
    public string StatusBooking { get; set; } = string.Empty;

    // Navigation properties
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public virtual ICollection<BookingHistory> BookingHistoriesAsOldStatus { get; set; } = new List<BookingHistory>();
    public virtual ICollection<BookingHistory> BookingHistoriesAsNewStatus { get; set; } = new List<BookingHistory>();
}