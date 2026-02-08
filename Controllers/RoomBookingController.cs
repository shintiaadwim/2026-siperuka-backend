using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomBookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomBookingController(AppDbContext context)
        {
            _context = context;
        }

        // ===============================
        // GET: api/peminjaman
        // ===============================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomBooking>>> GetAll()
        {
            var data = await _context.RoomBooking
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return Ok(data);
        }

        // ===============================
        // GET: api/peminjaman/{id}
        // ===============================
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomBooking>> GetById(int id)
        {
            var roomBooking = await _context.RoomBooking.FindAsync(id);

            if (roomBooking == null)
            {
                return NotFound(new { message = "Data peminjaman tidak ditemukan" });
            }

            return Ok(roomBooking);
        }

        // ===============================
        // POST: api/peminjaman
        // ===============================
        [HttpPost]
        public async Task<ActionResult<RoomBooking>> Create(RoomBooking roomBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RoomBooking.Add(roomBooking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = roomBooking.Id }, roomBooking);
        }

        // ===============================
        // PUT: api/peminjaman/{id}
        // ===============================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RoomBooking roomBooking)
        {
            if (id != roomBooking.Id)
            {
                return BadRequest(new { message = "ID tidak cocok" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(roomBooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.RoomBooking.Any(p => p.Id == id))
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

        // ===============================
        // DELETE: api/peminjaman/{id}
        // ===============================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var roomBooking = await _context.RoomBooking.FindAsync(id);

            if (roomBooking == null)
            {
                return NotFound(new { message = "Data peminjaman tidak ditemukan" });
            }

            _context.RoomBooking.Remove(roomBooking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Data peminjaman berhasil dihapus" });
        }
    }
}
