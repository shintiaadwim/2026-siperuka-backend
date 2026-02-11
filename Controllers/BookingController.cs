using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Backend.Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IBookingService _bookingService;

    public BookingController(AppDbContext context, IBookingService bookingService)
    {
        _context = context;
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingReadDto>>> GetAll()
    {
        var data = await _bookingService.GetAllBookingsAsync();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingReadDto>> GetById(int id)
    {
        var booking = await _bookingService.GetBookingByIdAsync(id);

        if (booking == null)
        {
            return NotFound(new { message = "Data peminjaman tidak ditemukan" });
        }

        return Ok(booking);
    }

    [HttpGet("filter/by-date")]
    public async Task<ActionResult<IEnumerable<BookingReadDto>>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (startDate > endDate)
        {
            return BadRequest(new { message = "Start date harus sebelum end date" });
        }

        var data = await _bookingService.GetBookingsByDateRangeAsync(startDate, endDate);
        return Ok(data);
    }

    [HttpGet("filter/by-status")]
    public async Task<ActionResult<IEnumerable<BookingReadDto>>> GetByStatus([FromQuery] int statusId)
    {
        var statusExists = await _context.BookingStatuses.AnyAsync(s => s.Id == statusId);
        if (!statusExists)
        {
            return BadRequest(new { message = "Status tidak ditemukan" });
        }

        var data = await _bookingService.GetBookingsByStatusAsync(statusId);
        return Ok(data);
    }

    [HttpGet("filter/by-user")]
    public async Task<ActionResult<IEnumerable<BookingReadDto>>> GetByUser([FromQuery] int userId)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == userId && u.DeletedAt == null);
        if (!userExists)
        {
            return BadRequest(new { message = "User tidak ditemukan" });
        }

        var data = await _bookingService.GetBookingsByUserAsync(userId);
        return Ok(data);
    }

    [HttpGet("filter/by-room")]
    public async Task<ActionResult<IEnumerable<BookingReadDto>>> GetByRoom([FromQuery] int roomId)
    {
        var roomExists = await _context.Rooms.AnyAsync(r => r.Id == roomId && r.DeletedAt == null);
        if (!roomExists)
        {
            return BadRequest(new { message = "Ruangan tidak ditemukan" });
        }

        var data = await _bookingService.GetBookingsByRoomAsync(roomId);
        return Ok(data);
    }

    [HttpGet("filter/by-date-and-room")]
    public async Task<ActionResult<IEnumerable<BookingReadDto>>> GetByDateAndRoom([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int roomId)
    {
        if (startDate > endDate)
        {
            return BadRequest(new { message = "Start date harus sebelum end date" });
        }

        var roomExists = await _context.Rooms.AnyAsync(r => r.Id == roomId && r.DeletedAt == null);
        if (!roomExists)
        {
            return BadRequest(new { message = "Ruangan tidak ditemukan" });
        }

        var data = await _bookingService.GetBookingsByDateAndRoomAsync(startDate, endDate, roomId);
        return Ok(data);
    }

    [HttpPost]
    public async Task<ActionResult<BookingReadDto>> Create(BookingCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (success, errorMessage, booking) = await _bookingService.CreateBookingAsync(dto);

        if (!success)
        {
            return BadRequest(new { message = errorMessage });
        }

        return CreatedAtAction(nameof(GetById), new { id = booking!.Id }, booking);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BookingUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (success, errorMessage) = await _bookingService.UpdateBookingAsync(id, dto);

        if (!success)
        {
            if (errorMessage == "Data peminjaman tidak ditemukan")
            {
                return NotFound(new { message = errorMessage });
            }
            return BadRequest(new { message = errorMessage });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var (success, errorMessage) = await _bookingService.DeleteBookingAsync(id);

        if (!success)
        {
            return NotFound(new { message = errorMessage });
        }

        return Ok(new { message = "Data peminjaman berhasil dihapus" });
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, BookingStatusUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (success, errorMessage) = await _bookingService.UpdateBookingStatusAsync(id, dto);

        if (!success)
        {
            if (errorMessage == "Data peminjaman tidak ditemukan")
            {
                return NotFound(new { message = errorMessage });
            }
            return BadRequest(new { message = errorMessage });
        }

        return Ok(new { message = "Status peminjaman berhasil diperbarui" });
    }

    #region Room Management

    [HttpGet("rooms")]
    public async Task<ActionResult<IEnumerable<RoomReadDto>>> GetRooms()
    {
        var data = await _context.Rooms
            .Where(r => r.DeletedAt == null)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new RoomReadDto
            {
                Id = r.Id,
                RoomCode = r.RoomCode,
                RoomName = r.RoomName,
                Capacity = r.Capacity,
                Location = r.Location,
                RoomStatus = r.RoomStatus
            })
            .ToListAsync();

        return Ok(data);
    }

    [HttpGet("rooms/{id}")]
    public async Task<ActionResult<RoomReadDto>> GetRoom(int id)
    {
        var room = await _context.Rooms
            .Where(r => r.Id == id && r.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (room == null)
        {
            return NotFound(new { message = "Data ruangan tidak ditemukan" });
        }

        return Ok(new RoomReadDto
        {
            Id = room.Id,
            RoomCode = room.RoomCode,
            RoomName = room.RoomName,
            Capacity = room.Capacity,
            Location = room.Location,
            RoomStatus = room.RoomStatus
        });
    }

    [HttpPost("rooms")]
    public async Task<ActionResult<RoomReadDto>> CreateRoom(RoomCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validasi unique RoomCode
        var existingRoom = await _context.Rooms.AnyAsync(r => r.RoomCode == dto.RoomCode && r.DeletedAt == null);
        if (existingRoom)
        {
            return BadRequest(new { message = "Kode ruangan sudah digunakan" });
        }

        var room = new Room
        {
            RoomCode = dto.RoomCode,
            RoomName = dto.RoomName,
            Capacity = dto.Capacity,
            Location = dto.Location,
            RoomStatus = dto.RoomStatus
        };

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, new RoomReadDto
        {
            Id = room.Id,
            RoomCode = room.RoomCode,
            RoomName = room.RoomName,
            Capacity = room.Capacity,
            Location = room.Location,
            RoomStatus = room.RoomStatus
        });
    }

    [HttpPut("rooms/{id}")]
    public async Task<IActionResult> UpdateRoom(int id, RoomUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var room = await _context.Rooms
            .Where(r => r.Id == id && r.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (room == null)
        {
            return NotFound(new { message = "Data ruangan tidak ditemukan" });
        }

        room.RoomName = dto.RoomName;
        room.Capacity = dto.Capacity;
        room.Location = dto.Location;
        room.RoomStatus = dto.RoomStatus;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("rooms/{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var room = await _context.Rooms
            .Where(r => r.Id == id && r.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (room == null)
        {
            return NotFound(new { message = "Data ruangan tidak ditemukan" });
        }

        // Soft delete
        room.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Data ruangan berhasil dihapus" });
    }

    [HttpGet("rooms/availability")]
    public async Task<ActionResult<IEnumerable<RoomAvailabilityDto>>> GetRoomAvailability([
        FromQuery] DateTime date,
        [FromQuery] TimeSpan startTime,
        [FromQuery] TimeSpan endTime)
    {
        if (endTime <= startTime)
        {
            return BadRequest(new { message = "EndTime harus setelah StartTime" });
        }

        var activeStatusIds = await _context.BookingStatuses
            .Where(s => s.StatusName == "Pending" || s.StatusName == "Approved")
            .Select(s => s.Id)
            .ToListAsync();

        var bookedRoomIds = await _context.Bookings
            .Where(b => b.DeletedAt == null
                && b.Date == date
                && b.StartTime < endTime
                && b.EndTime > startTime
                && activeStatusIds.Contains(b.StatusId))
            .Select(b => b.RoomId)
            .Distinct()
            .ToListAsync();

        var rooms = await _context.Rooms
            .Where(r => r.DeletedAt == null)
            .OrderBy(r => r.RoomName)
            .Select(r => new RoomAvailabilityDto
            {
                Id = r.Id,
                RoomCode = r.RoomCode,
                RoomName = r.RoomName,
                Capacity = r.Capacity,
                Location = r.Location,
                RoomStatus = r.RoomStatus,
                IsAvailable = r.RoomStatus == "Available" && !bookedRoomIds.Contains(r.Id)
            })
            .ToListAsync();

        return Ok(rooms);
    }

    #endregion
}
