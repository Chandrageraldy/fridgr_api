using FridgrAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FridgrAPI.Controllers
{
    [Route("api/Space")]
    [ApiController]
    public class SpaceController : Controller
    {
        private readonly FridgrDBContext _context;

        public SpaceController(FridgrDBContext context)
        {
            _context = context;
        }

        // GET: api/Space
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Space>>> GetSpace()
        {
            var spaces = await _context.Space.ToListAsync();

            return spaces;
        }

        // GET: api/Space/spaceId
        [HttpGet("{spaceId:int}")]
        public async Task<ActionResult<Space>> GetSpaceById(int spaceId)
        {
            var space = await _context.Space.FindAsync(spaceId);

            if (space == null)
            {
                return NotFound();

            }

            return space;
        }

        // GET: api/Space/userId
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Space>>> GetSpaceByUserId(string userId)
        {
            var space = await _context.Space
                            .Where(space => space.UserId == userId)
                            .ToListAsync();

            return space;
        }

        // GET: api/Space/exclude/spaceId/userId
        [HttpGet("exclude/{spaceId:int}/{userId}")]
        public async Task<ActionResult<IEnumerable<Space>>> GetSpacesExcludingById(int spaceId, string userId)
        {
            var spaces = await _context.Space
                                       .Where(space => space.UserId == userId && space.SpaceId != spaceId)
                                       .ToListAsync();

            return spaces;
        }

        // POST: api/Space
        [HttpPost]
        public async Task<ActionResult<Space>> PostSpace(Space space)
        {
            _context.Space.Add(space);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpaceById", new { spaceId = space.SpaceId }, space);
        }

        // PUT api/Space/spaceId
        [HttpPut("{spaceId}")]
        public async Task<ActionResult<Space>> PutSpace(int spaceId, Space space)
        {
            if (spaceId != space.SpaceId)
            {
                return BadRequest();
            }

            _context.Entry(space).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpaceExists(spaceId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return space;
        }

        private bool SpaceExists(int spaceId) => _context.Space.Any(e => e.SpaceId == spaceId);

        // DELETE api/Space/spaceId
        [HttpDelete("{spaceId}")]
        public async Task<ActionResult<Space>> DeleteSpace(int spaceId)
        {
            var space = await _context.Space.FindAsync(spaceId);

            if (space == null)
            {
                return NotFound();
            }

            _context.Space.Remove(space);
            await _context.SaveChangesAsync();

            return space;
        }
    }
}
