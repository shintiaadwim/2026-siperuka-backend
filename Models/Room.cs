using System;
using System.Collections.Generic;
namespace Backend.Models;

public class Room
{
    public int Id { get; set; }

    public string RoomCode { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;

    public int Capacity { get; set; }
    public string Location { get; set; } = string.Empty;
    public string RoomStatus { get; set; } = string.Empty;

    // Audit Fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }

    // Navigation properties
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}