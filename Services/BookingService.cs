using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.DTOs;
using Backend.Models;

namespace Backend.Services;

public class BookingService : IBookingService
{
    private readonly AppDbContext _context;

    public BookingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookingReadDto>> GetAllBookingsAsync()
    {
        return await _context.Bookings
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
                Purpose = b.Purpose,
                Room = new RoomReadDto
                {
                    Id = b.Room.Id,
                    RoomCode = b.Room.RoomCode,
                    RoomName = b.Room.RoomName,
                    Capacity = b.Room.Capacity,
                    Location = b.Room.Location,
                    RoomStatus = b.Room.RoomStatus
                }
            })
            .ToListAsync();
    }

    public async Task<BookingReadDto?> GetBookingByIdAsync(int id)
    {
        var booking = await _context.Bookings
            .Include(b => b.Room)
            .Where(b => b.Id == id && b.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (booking == null)
            return null;

        return new BookingReadDto
        {
            Id = booking.Id,
            RoomId = booking.RoomId,
            UserId = booking.UserId,
            StatusId = booking.StatusId,
            Date = booking.Date,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            Purpose = booking.Purpose,
            Room = new RoomReadDto
            {
                Id = booking.Room.Id,
                RoomCode = booking.Room.RoomCode,
                RoomName = booking.Room.RoomName,
                Capacity = booking.Room.Capacity,
                Location = booking.Room.Location,
                RoomStatus = booking.Room.RoomStatus
            }
        };
    }

    public async Task<IEnumerable<BookingReadDto>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Bookings
            .Where(b => b.DeletedAt == null && b.Date >= startDate && b.Date <= endDate)
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
                Purpose = b.Purpose,
                Room = new RoomReadDto
                {
                    Id = b.Room.Id,
                    RoomCode = b.Room.RoomCode,
                    RoomName = b.Room.RoomName,
                    Capacity = b.Room.Capacity,
                    Location = b.Room.Location,
                    RoomStatus = b.Room.RoomStatus
                }
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<BookingReadDto>> GetBookingsByStatusAsync(int statusId)
    {
        return await _context.Bookings
            .Where(b => b.DeletedAt == null && b.StatusId == statusId)
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
                Purpose = b.Purpose,
                Room = new RoomReadDto
                {
                    Id = b.Room.Id,
                    RoomCode = b.Room.RoomCode,
                    RoomName = b.Room.RoomName,
                    Capacity = b.Room.Capacity,
                    Location = b.Room.Location,
                    RoomStatus = b.Room.RoomStatus
                }
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<BookingReadDto>> GetBookingsByUserAsync(int userId)
    {
        return await _context.Bookings
            .Where(b => b.DeletedAt == null && b.UserId == userId)
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
                Purpose = b.Purpose,
                Room = new RoomReadDto
                {
                    Id = b.Room.Id,
                    RoomCode = b.Room.RoomCode,
                    RoomName = b.Room.RoomName,
                    Capacity = b.Room.Capacity,
                    Location = b.Room.Location,
                    RoomStatus = b.Room.RoomStatus
                }
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<BookingReadDto>> GetBookingsByRoomAsync(int roomId)
    {
        return await _context.Bookings
            .Where(b => b.DeletedAt == null && b.RoomId == roomId)
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
                Purpose = b.Purpose,
                Room = new RoomReadDto
                {
                    Id = b.Room.Id,
                    RoomCode = b.Room.RoomCode,
                    RoomName = b.Room.RoomName,
                    Capacity = b.Room.Capacity,
                    Location = b.Room.Location,
                    RoomStatus = b.Room.RoomStatus
                }
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<BookingReadDto>> GetBookingsByDateAndRoomAsync(DateTime startDate, DateTime endDate, int roomId)
    {
        return await _context.Bookings
            .Where(b => b.DeletedAt == null && b.Date >= startDate && b.Date <= endDate && b.RoomId == roomId)
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
                Purpose = b.Purpose,
                Room = new RoomReadDto
                {
                    Id = b.Room.Id,
                    RoomCode = b.Room.RoomCode,
                    RoomName = b.Room.RoomName,
                    Capacity = b.Room.Capacity,
                    Location = b.Room.Location,
                    RoomStatus = b.Room.RoomStatus
                }
            })
            .ToListAsync();
    }

    public async Task<(bool Success, string? ErrorMessage, BookingReadDto? Booking)> CreateBookingAsync(BookingCreateDto dto)
    {
        // Validasi Room exists
        var roomExists = await _context.Rooms.AnyAsync(r => r.Id == dto.RoomId && r.DeletedAt == null);
        if (!roomExists)
        {
            return (false, "Ruangan tidak ditemukan", null);
        }

        // Validasi User exists
        var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId && u.DeletedAt == null);
        if (!userExists)
        {
            return (false, "User tidak ditemukan", null);
        }

        // Get default status (Pending)
        var defaultStatus = await _context.BookingStatuses.FirstOrDefaultAsync(s => s.StatusName == "Pending");
        if (defaultStatus == null)
        {
            return (false, "Status default tidak ditemukan", null);
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

        var room = await _context.Rooms
            .Where(r => r.Id == booking.RoomId && r.DeletedAt == null)
            .FirstOrDefaultAsync();

        var bookingDto = new BookingReadDto
        {
            Id = booking.Id,
            RoomId = booking.RoomId,
            UserId = booking.UserId,
            StatusId = booking.StatusId,
            Date = booking.Date,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            Purpose = booking.Purpose,
            Room = room == null
                ? null
                : new RoomReadDto
                {
                    Id = room.Id,
                    RoomCode = room.RoomCode,
                    RoomName = room.RoomName,
                    Capacity = room.Capacity,
                    Location = room.Location,
                    RoomStatus = room.RoomStatus
                }
        };

        return (true, null, bookingDto);
    }

    public async Task<(bool Success, string? ErrorMessage)> UpdateBookingAsync(int id, BookingUpdateDto dto)
    {
        var booking = await _context.Bookings
            .Where(b => b.Id == id && b.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (booking == null)
        {
            return (false, "Data peminjaman tidak ditemukan");
        }

        // Validasi Status exists
        var statusExists = await _context.BookingStatuses.AnyAsync(s => s.Id == dto.StatusId);
        if (!statusExists)
        {
            return (false, "Status tidak ditemukan");
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

        return (true, null);
    }

    public async Task<(bool Success, string? ErrorMessage)> DeleteBookingAsync(int id)
    {
        var booking = await _context.Bookings
            .Where(b => b.Id == id && b.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (booking == null)
        {
            return (false, "Data peminjaman tidak ditemukan");
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

        return (true, null);
    }

    public async Task<(bool Success, string? ErrorMessage)> UpdateBookingStatusAsync(int id, BookingStatusUpdateDto dto)
    {
        var booking = await _context.Bookings
            .Where(b => b.Id == id && b.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (booking == null)
        {
            return (false, "Data peminjaman tidak ditemukan");
        }

        // Validasi status exists
        var statusExists = await _context.BookingStatuses.AnyAsync(s => s.Id == dto.NewStatusId);
        if (!statusExists)
        {
            return (false, "Status tidak ditemukan");
        }

        // Jika status sama, tidak perlu update
        if (booking.StatusId == dto.NewStatusId)
        {
            return (false, "Status sudah sama");
        }

        var oldStatusId = booking.StatusId;
        booking.StatusId = dto.NewStatusId;

        await _context.SaveChangesAsync();

        // Catat perubahan status ke history
        _context.BookingHistories.Add(new BookingHistory
        {
            BookingId = booking.Id,
            OldStatus = oldStatusId,
            NewStatus = booking.StatusId,
            ChangedBy = booking.UserId,
            Note = dto.Note
        });
        await _context.SaveChangesAsync();

        return (true, null);
    }
}
