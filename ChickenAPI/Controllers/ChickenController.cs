using ChickenAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChickenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ChickenController : ControllerBase
    {
        private readonly FarmDbContext _context;

        public ChickenController(FarmDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chicken>>> GetChickens()
        {
            return await _context.Chickens.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chicken>> GetById(int id)
        {
            var chicken = await _context.Chickens.FindAsync(id);

            if (chicken == null)

                return NotFound();


            return chicken;
        }

        [HttpPost]
        public async Task<ActionResult<Chicken>> Create(Chicken chicken)
        {
            _context.Chickens.Add(chicken);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = chicken.ChickId }, chicken);
        }

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
                if (!ChickenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

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

        private bool ChickenExists(int id)
        {
            return _context.Chickens.Any(e => e.ChickId == id);
        }
    }
}