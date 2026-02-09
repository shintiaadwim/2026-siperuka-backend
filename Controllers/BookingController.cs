using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
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

    [HttpGet] // GET: api/peminjaman
    public async Task<ActionResult<IEnumerable<Booking>>> GetAll()
    {
        var data = await _context.Bookings
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return Ok(data);
    }

    [HttpGet("{id}")] // GET: api/peminjaman/{id}
    public async Task<ActionResult<Booking>> GetById(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);

        if (booking == null)
        {
            return NotFound(new { message = "Data peminjaman tidak ditemukan" });
        }

        return Ok(booking);
    }

    [HttpPost] // POST: api/peminjaman
    public async Task<ActionResult<Booking>> Create(Booking booking)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
    }

    [HttpPut("{id}")] // PUT: api/peminjaman/{id}
    public async Task<IActionResult> Update(int id, Booking booking)
    {
        if (id != booking.Id)
        {
            return BadRequest(new { message = "ID tidak cocok" });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Entry(booking).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Bookings.Any(p => p.Id == id))
            {
                return NotFound(new { message = "Data peminjaman tidak ditemukan" });
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")] // DELETE: api/peminjaman/{id}
    public async Task<IActionResult> Delete(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);

        if (booking == null)
        {
            return NotFound(new { message = "Data peminjaman tidak ditemukan" });
        }

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Data peminjaman berhasil dihapus" });
    }
}