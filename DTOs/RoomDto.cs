using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class RoomCreateDto
{
    [Required]
    [MaxLength(50)]
    public string RoomCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string RoomName { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }

    [Required]
    [MaxLength(255)]
    public string Location { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string RoomStatus { get; set; } = string.Empty;
}

public class RoomReadDto
{
    public int Id { get; set; }
    public string RoomCode { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Location { get; set; } = string.Empty;
    public string RoomStatus { get; set; } = string.Empty;
}

public class RoomUpdateDto
{
    [Required]
    [MaxLength(100)]
    public string RoomName { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }

    [Required]
    [MaxLength(255)]
    public string Location { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string RoomStatus { get; set; } = string.Empty;
}