using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.DTOs;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly AppDbContext _context;

    public RoomController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomReadDto>>> GetRooms()
    {
        var data = await _context.Rooms
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

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomReadDto>> GetRoom(int id)
    {
        var room = await _context.Rooms.FindAsync(id);

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

    [HttpPost]
    public async Task<ActionResult<RoomReadDto>> CreateRoom(RoomCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoom(int id, RoomUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var room = await _context.Rooms.FindAsync(id);
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var room = await _context.Rooms.FindAsync(id);

        if (room == null)
        {
            return NotFound(new { message = "Data ruangan tidak ditemukan" });
        }

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Data ruangan berhasil dihapus" });
    }
}
