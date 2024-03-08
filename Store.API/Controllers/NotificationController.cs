using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data;
using Store.Infrastructure.Entities;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly StoreContext _context;

        public NotificationController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }

        // GET: api/notifications/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return notification;
        }

        // POST: api/notifications
        [HttpPost]
        public async Task<ActionResult<Notification>> CreateNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotification), new { id = notification.NotificationID }, notification);
        }

        // PUT: api/notifications/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, Notification notification)
        {
            if (id != notification.NotificationID)
            {
                return BadRequest();
            }

            _context.Entry(notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
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

        // DELETE: api/notifications/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificationExists(int id)
        {
            return _context.Notifications.Any(e => e.NotificationID == id);
        }
    }
}