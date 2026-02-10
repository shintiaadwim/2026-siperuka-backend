using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.DTOs;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly AppDbContext _context;

    public BookingController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingReadDto>>> GetAll()
    {
        var data = await _context.Bookings
            .Where(b => b.DeletedAt == null)
            .OrderByDescending(p => p.CreatedAt)
            .Select(b => new BookingReadDto
            {
                Id = b.Id,
                RoomId = b.RoomId,
                UserId = b.UserId,
                StatusId = b.StatusId,
                Date = b.Date,
                StartTime = b.StartTime,
                EndTime = b.EndTime,
                Purpose = b.Purpose
            })
            .ToListAsync();

        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingReadDto>> GetById(int id)
    {
        var booking = await _context.Bookings
            .Where(b => b.Id == id && b.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (booking == null)
        {
            return NotFound(new { message = "Data peminjaman tidak ditemukan" });
        }

        return Ok(new BookingReadDto
        {
            Id = booking.Id,
            RoomId = booking.RoomId,
            UserId = booking.UserId,
            StatusId = booking.StatusId,
            Date = booking.Date,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            Purpose = booking.Purpose
        });
    }

    [HttpPost]
    public async Task<ActionResult<BookingReadDto>> Create(BookingCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validasi Room exists
        var roomExists = await _context.Rooms.AnyAsync(r => r.Id == dto.RoomId && r.DeletedAt == null);
        if (!roomExists)
        {
            return BadRequest(new { message = "Ruangan tidak ditemukan" });
        }

        // Validasi User exists
        var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId && u.DeletedAt == null);
        if (!userExists)
        {
            return BadRequest(new { message = "User tidak ditemukan" });
        }

        // Get default status (Pending)
        var defaultStatus = await _context.BookingStatuses.FirstOrDefaultAsync(s => s.StatusName == "Pending");
        if (defaultStatus == null)
        {
            return BadRequest(new { message = "Status default tidak ditemukan" });
        }

        var booking = new Booking
        {
            RoomId = dto.RoomId,
            UserId = dto.UserId,
            StatusId = defaultStatus.Id,
            Date = dto.Date,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Purpose = dto.Purpose
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        _context.BookingHistories.Add(new BookingHistory
        {
            BookingId = booking.Id,
            OldStatus = booking.StatusId,
            NewStatus = booking.StatusId,
            ChangedBy = booking.UserId,
            Note = "Created booking"
        });
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = booking.Id }, new BookingReadDto
        {
            Id = booking.Id,
            RoomId = booking.RoomId,
            UserId = booking.UserId,
            StatusId = booking.StatusId,
            Date = booking.Date,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            Purpose = booking.Purpose
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BookingUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var booking = await _context.Bookings
            .Where(b => b.Id == id && b.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (booking == null)
        {
            return NotFound(new { message = "Data peminjaman tidak ditemukan" });
        }

        // Validasi Status exists
        var statusExists = await _context.BookingStatuses.AnyAsync(s => s.Id == dto.StatusId);
        if (!statusExists)
        {
            return BadRequest(new { message = "Status tidak ditemukan" });
        }

        var oldStatusId = booking.StatusId;

        booking.Date = dto.Date;
        booking.StartTime = dto.StartTime;
        booking.EndTime = dto.EndTime;
        booking.Purpose = dto.Purpose;
        booking.StatusId = dto.StatusId;

        await _context.SaveChangesAsync();

        _context.BookingHistories.Add(new BookingHistory
        {
            BookingId = booking.Id,
            OldStatus = oldStatusId,
            NewStatus = booking.StatusId,
            ChangedBy = booking.UserId,
            Note = "Updated booking"
        });
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var booking = await _context.Bookings
            .Where(b => b.Id == id && b.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (booking == null)
        {
            return NotFound(new { message = "Data peminjaman tidak ditemukan" });
        }

        // Soft delete
        booking.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        _context.BookingHistories.Add(new BookingHistory
        {
            BookingId = booking.Id,
            OldStatus = booking.StatusId,
            NewStatus = booking.StatusId,
            ChangedBy = booking.UserId,
            Note = "Deleted booking"
        });
        await _context.SaveChangesAsync();

        return Ok(new { message = "Data peminjaman berhasil dihapus" });
    }
}