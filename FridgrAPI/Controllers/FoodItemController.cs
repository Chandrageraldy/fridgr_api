using FridgrAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FridgrAPI.Controllers
{
    [Route("api/FoodItem")]
    [ApiController]
    public class FoodItemController : Controller
    {
        private readonly FridgrDBContext _context;

        public FoodItemController(FridgrDBContext context)
        {
            _context = context;
        }

        // GET: api/FoodItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodItem>>> GetFoodItem()
        {
            var foodItems = await _context.FoodItem.ToListAsync();

            return foodItems;
        }

        // GET: api/FoodItem/foodItemId
        [HttpGet("{foodItemId:int}")]
        public async Task<ActionResult<FoodItem>> GetFoodItemById(int foodItemId)
        {
            var foodItem = await _context.FoodItem.FindAsync(foodItemId);

            if (foodItem == null)
            {
                return NotFound();
            }

            return foodItem;
        }

        // GET: api/FoodItem/Space/spaceId
        [HttpGet("Space/{spaceId:int}")]
        public async Task<ActionResult<IEnumerable<FoodItem>>> GetFoodItemsBySpaceId(int spaceId)
        {
            var foodItems = await _context.FoodItem
                .Where(i => i.SpaceId == spaceId)
                .ToListAsync();

            return foodItems;
        }

        // POST: api/FoodItem
        [HttpPost]
        public async Task<ActionResult<FoodItem>> PostFoodItem(FoodItem foodItem)
        {
            _context.FoodItem.Add(foodItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoodItemById", new { foodItemId = foodItem.FoodItemId }, foodItem);
        }

        // PUT api/FoodItem/foodItemId
        [HttpPut("{foodItemId}")]
        public async Task<ActionResult<FoodItem>> PutFoodItem(int foodItemId, FoodItem foodItem)
        {
            if (foodItemId != foodItem.FoodItemId)
            {
                return BadRequest();
            }

            _context.Entry(foodItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodItemExists(foodItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return foodItem;
        }
        private bool FoodItemExists(int foodItemId) => _context.FoodItem.Any(e => e.FoodItemId == foodItemId);

        // DELETE api/FoodItem/foodItemId
        [HttpDelete("{foodItemId}")]
        public async Task<ActionResult<FoodItem>> DeleteFoodItem(int foodItemId)
        {
            var foodItem = await _context.FoodItem.FindAsync(foodItemId);

            if (foodItem == null)
            {
                return NotFound();
            }

            _context.FoodItem.Remove(foodItem);
            await _context.SaveChangesAsync();

            return foodItem;
        }
    }
}
