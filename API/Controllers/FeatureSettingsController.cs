using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeatureSettingsController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly ILogger<InvoiceController> _logger;
        private readonly IMapper _mapper;

        public FeatureSettingsController(StoreContext context, IMapper mapper, ILogger<InvoiceController> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        // GET api/featureSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeatureSettings>>> Get()
        {
            var featureSettings = await _context.FeatureSettings.OrderByDescending(f => f.Id).ToListAsync();
            return featureSettings;
        }

        // GET api/featureSettings/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FeatureSettings>> Get(int id)
        {
            var featureSetting = await _context.FeatureSettings.FirstOrDefaultAsync(fs => fs.Id == id);
            if (featureSetting == null)
            {
                return NotFound();
            }
            return featureSetting;
        }

        // POST api/featureSettings
        [HttpPost]
        public async Task<ActionResult<FeatureSettings>> Post(FeatureSettings featureSetting)
        {
            _context.FeatureSettings.Add(featureSetting);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = featureSetting.Id }, featureSetting);
        }

        // PUT api/featureSettings/{id}
        // [HttpPut("{id}")]
        // public async Task<IActionResult> Put(int id, FeatureSettings featureSetting)
        // {
        //     var existingFeatureSetting = await _context.FeatureSettings.FirstOrDefaultAsync(fs => fs.Id == id);
        //     if (existingFeatureSetting == null)
        //     {
        //         return NotFound();
        //     }
        //     existingFeatureSetting.IsFeatureEnabled = featureSetting.IsFeatureEnabled;
        //     existingFeatureSetting.FeatureName = featureSetting.FeatureName;
        //     existingFeatureSetting.FeatureDescription = featureSetting.FeatureDescription;
        //     existingFeatureSetting.DisplayOrder = featureSetting.DisplayOrder;
        //     existingFeatureSetting.FeatureIcon = featureSetting.FeatureIcon;
        //     existingFeatureSetting.FeatureRoute = featureSetting.FeatureRoute;
        //     existingFeatureSetting.FeatureType = featureSetting.FeatureType;
        //     existingFeatureSetting.FeatureCategory = featureSetting.FeatureCategory;
        //     existingFeatureSetting.ParentFeatureId = featureSetting.ParentFeatureId;
        //     existingFeatureSetting.AdminFeature = featureSetting.AdminFeature;
        //     existingFeatureSetting.EnabledForRoles = featureSetting.EnabledForRoles;
        //     await _context.SaveChangesAsync();
        //     return NoContent();
        // }

        [HttpPut("{id}", Name = "ChangeStatus")]
        public async Task<IActionResult> SetFeatureStatus(int id, FeatureStatusDto status)
        {
            try
            {
                var feature = await _context.FeatureSettings
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

                if (feature == null)
                {
                    return NotFound();
                }

                feature.IsFeatureEnabled = status.IsFeatureEnabled;

                _context.Entry(feature).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, AddErrorDetails(ex, "An error occurred while updating invoice settings."));
                //Return a 500 Internal Server Error status code
                return StatusCode(500, "Internal server error");
            }
            return NoContent();
        }

        // DELETE api/featureSettings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var featureSetting = await _context.FeatureSettings.FirstOrDefaultAsync(fs => fs.Id == id);
            if (featureSetting == null)
            {
                return NotFound();
            }
            _context.FeatureSettings.Remove(featureSetting);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #region Private Methods
        private string AddErrorDetails(Exception ex, string message = "")
        {
            return message + " " + User.Identity.Name + " : " + DateTime.UtcNow.ToString() + " " + ex.Message + " " + ex.InnerException.Message + " " + ex.InnerException.InnerException.Message;
        }
        #endregion
    }
}