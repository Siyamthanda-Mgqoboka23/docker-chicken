using ChickenAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChickenAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ChickensController : ControllerBase
    {
        private readonly FarmDbContext _context;

        public ChickensController(FarmDbContext context)
        {
            _context = context;
        }

        // GET: api/chickens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chicken>>> GetAll()
        {
            return await _context.Chickens.ToListAsync();
        }

        // GET: api/chickens/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Chicken>> GetById(int id)
        {
            var chicken = await _context.Chickens.FindAsync(id);
            if (chicken == null)
            {
                return NotFound();
            }
            return chicken;
        }

        // POST: api/chickens
        [HttpPost]
        public async Task<ActionResult<Chicken>> Create(Chicken chicken)
        {
            _context.Chickens.Add(chicken);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = chicken.ChickId }, chicken);
        }

        // PUT: api/chickens/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Chicken chicken)
        {
            if (id != chicken.ChickId)
            {
                return BadRequest();
            }

            _context.Entry(chicken).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Chickens.Any(e => e.ChickId == id))
                {
                    return NotFound();
                }

                throw;

            }
            return NoContent();
        }

        // DELETE: api/chickens/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var chicken = await _context.Chickens.FindAsync(id);
            if (chicken == null)
            {
                return NotFound();
            }

            _context.Chickens.Remove(chicken);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
