using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class BookingStatusUpdateDto
{
    [Range(1, int.MaxValue)]
    public int NewStatusId { get; set; }

    [MaxLength(500)]
    public string Note { get; set; } = string.Empty;
}
