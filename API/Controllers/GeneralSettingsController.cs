using API.BL;  
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Infrastructure.Data.DTOs.Settings;
using Store.Infrastructure.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneralSettingsController : ControllerBase
    { 
        private readonly IGeneralSettingsBL _generalSettingsBL;
        public GeneralSettingsController(IGeneralSettingsBL generalSettingsBL)
        { 
            _generalSettingsBL = generalSettingsBL;
        }

        // GET: api/GeneralSettings
        [HttpGet]
        public async Task<ActionResult<GeneralSettings>> GetGeneralSettings()
        {
            var generalSettings = await _generalSettingsBL.GetGeneralSettings();
            return generalSettings;
        }

        [HttpGet("getAppName")]
        public async Task<ActionResult<string>> GetAppName()
        {
            var appName = await _generalSettingsBL.GetAppName();
            return appName;
        }

        // GET: api/GeneralSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralSettings>> GetGeneralSettings(int id)
        {
            var generalSettings = await _generalSettingsBL.GetGeneralSettings(id);

            if (generalSettings == null)
            {
                return NotFound();
            }

            return generalSettings;
        }

        // POST: api/GeneralSettings 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<GeneralSettings>> PostGeneralSettings([FromForm] GeneralSettingsDto generalSettingsDto)
        { 
            var (generalSettings, result) = await _generalSettingsBL.PostGeneralSettings(generalSettingsDto, User);

            if (result) return CreatedAtAction(nameof(GetGeneralSettings), new { id = generalSettingsDto.Id }, generalSettingsDto);

            return BadRequest(new ProblemDetails { Title = "Problem creating new product" });
        }

        // PUT: api/GeneralSettings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeneralSettings(int id, [FromForm] GeneralSettingsDto generalSettingsDto)
        {
            await _generalSettingsBL.PutGeneralSettings(id, generalSettingsDto, User);

            return NoContent();
        }

        // DELETE: api/GeneralSettings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneralSettings(int id)
        {
            await _generalSettingsBL.DeleteGeneralSettings(id);

            return NoContent();
        }
    }
}