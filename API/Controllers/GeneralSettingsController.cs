using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneralSettingsController : ControllerBase
    {
        private readonly StoreContext _context;

        public GeneralSettingsController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/GeneralSettings
        [HttpGet]
        public async Task<ActionResult<GeneralSettings>> GetGeneralSettings()
        {
            var generalSettings = await _context.GeneralSettings.FirstOrDefaultAsync();
            return generalSettings;
        }

        // GET: api/GeneralSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralSettings>> GetGeneralSettings(int id)
        {
            var generalSettings = await _context.GeneralSettings.FindAsync(id);

            if (generalSettings == null)
            {
                return new GeneralSettings();
            }

            return generalSettings;
        }

        // POST: api/GeneralSettings
        [HttpPost]
        public async Task<ActionResult<GeneralSettings>> PostGeneralSettings(GeneralSettings generalSettings)
        {
            _context.GeneralSettings.Add(generalSettings);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGeneralSettings), new { id = generalSettings.Id }, generalSettings);
        }

        // PUT: api/GeneralSettings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeneralSettings(int id, GeneralSettings generalSettings)
        {
            if (id != generalSettings.Id)
            {
                return BadRequest();
            }

            _context.Entry(generalSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneralSettingsExists(id))
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

        // DELETE: api/GeneralSettings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneralSettings(int id)
        {
            var generalSettings = await _context.GeneralSettings.FindAsync(id);
            if (generalSettings == null)
            {
                return NotFound();
            }

            _context.GeneralSettings.Remove(generalSettings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeneralSettingsExists(int id)
        {
            return _context.GeneralSettings.Any(e => e.Id == id);
        }
        
    }
}