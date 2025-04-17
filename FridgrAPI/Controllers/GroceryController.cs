using FridgrAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FridgrAPI.Controllers
{
    [Route("api/Grocery")]
    [ApiController]
    public class GroceryController : Controller
    {
        private readonly FridgrDBContext _context;

        public GroceryController(FridgrDBContext context)
        {
            _context = context;
        }

        // GET: api/Grocery
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grocery>>> GetGrocery()
        {
            var groceries = await _context.Grocery.ToListAsync();

            return groceries;
        }

        // GET: api/Grocery/groceryId
        [HttpGet("{groceryId:int}")]
        public async Task<ActionResult<Grocery>> GetGroceryById(int groceryId)
        {
            var grocery = await _context.Grocery.FindAsync(groceryId);

            if (grocery == null)
            {
                return NotFound();
            }

            return grocery;
        }

        // GET: api/Grocery/userId
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Grocery>>> GetGroceryByUserId(string userId)
        {
            var grocery = await _context.Grocery
                            .Where(grocery => grocery.UserId == userId)
                            .ToListAsync();

            return grocery;
        }

        // POST: api/Grocery
        [HttpPost]
        public async Task<ActionResult<Grocery>> PostGrocery(Grocery grocery)
        {
            _context.Grocery.Add(grocery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroceryById", new { groceryId = grocery.GroceryId }, grocery);
        }

        // PUT api/Grocery/groceryId
        [HttpPut("{groceryId}")]
        public async Task<ActionResult<Grocery>> PutGrocery(int groceryId, Grocery grocery)
        {
            if (groceryId != grocery.GroceryId)
            {
                return BadRequest();
            }

            _context.Entry(grocery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroceryExists(groceryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return grocery;
        }

        private bool GroceryExists(int groceryId) => _context.Grocery.Any(e => e.GroceryId == groceryId);

        // DELETE api/Grocery/groceryId
        [HttpDelete("{groceryId}")]
        public async Task<ActionResult<Grocery>> DeleteGrocery(int groceryId)
        {
            var grocery = await _context.Grocery.FindAsync(groceryId);

            if (grocery == null)
            {
                return NotFound();
            }

            _context.Grocery.Remove(grocery);
            await _context.SaveChangesAsync();

            return grocery;
        }
    }
}
