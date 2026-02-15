using Backend.Models;

namespace Backend.DTOs;

public class BookingStatusDto
{
    public int Id { get; set; }
    public string StatusBooking { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;

    public BookingStatusDto() { }
    public BookingStatusDto(BookingStatus status)
    {
        Id = status.Id;
        StatusBooking = status.StatusBooking;
        StatusName = status.StatusName;
    }
}
