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

    #region Booking Endpoints
    #region Booking Management

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = _context.Bookings
            .Include(b => b.Status)
            .Include(b => b.User)
            .Include(b => b.Room)
            .Where(b => b.DeletedAt == null)
            .OrderByDescending(b => b.Date);

        var total = await query.CountAsync();
        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(b => new BookingReadDto
            {
                Id = b.Id,
                RoomId = b.RoomId,
                UserId = b.UserId,
                StatusId = b.StatusId,
                Date = b.Date,
                StartTime = b.StartTime,
                EndTime = b.EndTime,
                Purpose = b.Purpose,
                Room = new RoomReadDto
                {
                    Id = b.Room.Id,
                    RoomCode = b.Room.RoomCode,
                    RoomName = b.Room.RoomName,
                    Capacity = b.Room.Capacity,
                    Location = b.Room.Location,
                    RoomStatus = b.Room.RoomStatus
                },
                Status = b.Status == null ? null : new BookingStatusDto
                {
                    Id = b.Status.Id,
                    StatusBooking = b.Status.StatusBooking,
                    StatusName = b.Status.StatusName
                },
                User = b.User == null ? null : new UserReadDto
                {
                    Id = b.User.Id,
                    Name = b.User.Name,
                    Email = b.User.Email
                }
            })
            .ToListAsync();

        return Ok(new
        {
            data,
            total,
            page,
            pageSize
        });
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

    [HttpGet("status")]
    public async Task<ActionResult<IEnumerable<BookingStatusDto>>> GetAllStatus()
    {
        var statuses = await _context.BookingStatuses
            .OrderBy(s => s.Id)
            .Select(s => new BookingStatusDto(s))
            .ToListAsync();
        return Ok(statuses);
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

    #endregion
    #region Room Endpoints
    #region Room Management

    [HttpGet("history")]
    public async Task<ActionResult<dynamic>> GetBookingHistory([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.BookingHistories
                .Include(h => h.Booking)
                    .ThenInclude(b => b.Room)
                .Include(h => h.Booking)
                    .ThenInclude(b => b.User)
                .Include(h => h.OldStatusNavigation)
                .Include(h => h.NewStatusNavigation)
                .OrderByDescending(h => h.ChangedAt);

            var total = await query.CountAsync();
            var history = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = history.Select(h => new
            {
                h.Id,
                h.BookingId,
                Booking = h.Booking != null ? new
                {
                    h.Booking.Id,
                    h.Booking.Date,
                    h.Booking.StartTime,
                    h.Booking.EndTime,
                    h.Booking.Purpose,
                    Room = h.Booking.Room != null ? new
                    {
                        h.Booking.Room.Id,
                        h.Booking.Room.RoomName,
                        h.Booking.Room.RoomCode
                    } : null,
                    User = h.Booking.User != null ? new
                    {
                        h.Booking.User.Id,
                        h.Booking.User.Name
                    } : null
                } : null,
                OldStatus = h.OldStatusNavigation?.StatusName ?? "",
                NewStatus = h.NewStatusNavigation?.StatusName ?? "",
                h.ChangedField,
                h.OldValue,
                h.NewValue,
                h.EntityType,
                h.ChangedAt,
                h.Note
            }).ToList();

            return Ok(new
            {
                data = result,
                total,
                page,
                pageSize
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error fetching history", error = ex.Message });
        }
    }

    #region Rooms

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

    #endregion

    #region User Endpoints

    [HttpGet("users")]
    public async Task<ActionResult<UserListResponse>> GetAllUsers()
    {
        var users = await _context.Users
            .Where(u => u.DeletedAt == null)
            .OrderByDescending(u => u.CreatedAt)
            .Select(u => new UserReadDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            })
            .ToListAsync();

        var response = new UserListResponse
        {
            Total = users.Count,
            Page = 1,
            PageSize = users.Count,
            Data = users
        };

        return Ok(response);
    }

    [HttpGet("users/{id}")]
    public async Task<ActionResult<UserReadDto>> GetUserById(int id)
    {
        var user = await _context.Users
            .Where(u => u.Id == id && u.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound(new { message = "User tidak ditemukan" });
        }

        return Ok(new UserReadDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        });
    }

    [HttpPost("users")]
    public async Task<ActionResult<UserReadDto>> CreateUser(UserCreateDto dto)
    {
        // Cek apakah user dengan nama sama sudah ada
        var existingUser = await _context.Users
            .Where(u => u.Name == dto.Name && u.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (existingUser != null)
        {
            return Ok(new UserReadDto
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Email = existingUser.Email
            });
        }

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email ?? string.Empty,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new UserReadDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        });
    }

    [HttpPut("users/{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null);
        if (user == null)
            return NotFound(new { message = "User tidak ditemukan" });

        user.Name = dto.Name;
        user.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new UserReadDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        });
    }

    #endregion
}
