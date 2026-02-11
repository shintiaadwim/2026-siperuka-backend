using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Backend.Converters;
using Backend.Data;
using System.Collections.Generic;

namespace Backend.DTOs;

public class BookingCreateDto : IValidatableObject
{
    [Range(1, int.MaxValue)]
    public int RoomId { get; set; }

    [Range(1, int.MaxValue)]
    public int UserId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Purpose { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [JsonConverter(typeof(TimeSpanConverter))]
    public TimeSpan StartTime { get; set; }

    [Required]
    [JsonConverter(typeof(TimeSpanConverter))]
    public TimeSpan EndTime { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "EndTime harus setelah StartTime.",
                new[] { nameof(EndTime) });
            yield break;
        }
        var dbContext = validationContext.GetService(typeof(AppDbContext)) as AppDbContext;
        if (dbContext is null)
        {
            yield break;
        }
        var hasOverlap = dbContext.Bookings.Any(
            b => b.RoomId == RoomId
                && b.DeletedAt == null
                && b.Date == Date
                && b.StartTime < EndTime
                && b.EndTime > StartTime);
        if (hasOverlap)
        {
            yield return new ValidationResult(
                "Room sudah terbooking pada rentang waktu tersebut.",
                new[] { nameof(StartTime), nameof(EndTime) });
        }
    }
}

public class BookingReadDto
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public int UserId { get; set; }
    public int StatusId { get; set; }
    public DateTime Date { get; set; }
    [JsonConverter(typeof(TimeSpanConverter))]
    public TimeSpan StartTime { get; set; }
    [JsonConverter(typeof(TimeSpanConverter))]
    public TimeSpan EndTime { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public RoomReadDto? Room { get; set; }
}

public class BookingUpdateDto : IValidatableObject
{
    [Required]
    public DateTime Date { get; set; }
    [Required]
    [JsonConverter(typeof(TimeSpanConverter))]
    public TimeSpan StartTime { get; set; }
    [Required]
    [JsonConverter(typeof(TimeSpanConverter))]
    public TimeSpan EndTime { get; set; }
    [Required]
    [MaxLength(255)]
    public string Purpose { get; set; } = string.Empty;
    [Required]
    public int StatusId { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "EndTime harus setelah StartTime.",
                new[] { nameof(EndTime) });
        }
    }
}