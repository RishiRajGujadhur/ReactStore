using API.Data;
using API.DTOs;
using API.Entities;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneralSettingsController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;

        public GeneralSettingsController(StoreContext context, IMapper mapper, ImageService imageService)
        {
            _imageService = imageService;
            _mapper = mapper;
            _context = context;
        }

        // GET: api/GeneralSettings
        [HttpGet]
        public async Task<ActionResult<GeneralSettings>> GetGeneralSettings()
        {
            var generalSettings = await _context.GeneralSettings.FirstOrDefaultAsync();
            return generalSettings;
        }

        [HttpGet("getAppName")]
        public async Task<ActionResult<string>> GetAppName()
        {
            var generalSettings = await _context.GeneralSettings.FirstOrDefaultAsync();
            return generalSettings.AppName;
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<GeneralSettings>> PostGeneralSettings([FromForm] GeneralSettingsDto generalSettingsDto)
        { 
            if (generalSettingsDto.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(generalSettingsDto.File);

                if (imageResult.Error != null) return BadRequest(new ProblemDetails
                {
                    Title = imageResult.Error.Message
                });

                generalSettingsDto.LogoURL = imageResult.SecureUrl.ToString();
                generalSettingsDto.PublicId = imageResult.PublicId;
            }

            var generalSettings = _mapper.Map<GeneralSettings>(generalSettingsDto);
            _context.GeneralSettings.Add(generalSettings);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtAction(nameof(GetGeneralSettings), new { id = generalSettingsDto.Id }, generalSettingsDto);

            return BadRequest(new ProblemDetails { Title = "Problem creating new product" });
        }

        // PUT: api/GeneralSettings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeneralSettings(int id, [FromForm] GeneralSettingsDto generalSettingsDto)
        {
            if (id != generalSettingsDto.Id)
            {
                return BadRequest();
            }
            
            if (generalSettingsDto.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(generalSettingsDto.File);

                if (imageResult.Error != null) return BadRequest(new ProblemDetails
                {
                    Title = imageResult.Error.Message
                });

                generalSettingsDto.LogoURL = imageResult.SecureUrl.ToString();
                generalSettingsDto.PublicId = imageResult.PublicId;
            } 
            
            var generalSettings = _mapper.Map<GeneralSettings>(generalSettingsDto);
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