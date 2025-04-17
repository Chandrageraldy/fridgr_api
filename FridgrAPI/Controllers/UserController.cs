using FridgrAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FridgrAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly FridgrDBContext _context;

        public UserController(FridgrDBContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var users = await _context.User.ToListAsync();

            return users;
        }

        // GET: api/User/userId
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserById(string userId)
        {
            var user = await _context.User.FindAsync(userId);

            if (user == null)
            {
                return NotFound();

            }

            return user;
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserById", new { userId = user.UserId}, user);
        }

        // PUT api/User/userId
        [HttpPut("{userId}")]
        public async Task<ActionResult<User>> PutUser(string userId, User user)
        {
            if (userId != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return user;
        }
        private bool UserExists(string userId) => _context.User.Any(e => e.UserId == userId);

        // DELETE api/Item/itemId
        [HttpDelete("{userId}")]
        public async Task<ActionResult<User>> DeleteUser(string userId)
        {
            var user = await _context.User.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
