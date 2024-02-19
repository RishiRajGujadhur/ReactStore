using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Data;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeatureSettingsController : ControllerBase
    {
        private readonly StoreContext _context;
        
        private readonly IMapper _mapper;

        public FeatureSettingsController(StoreContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET api/featureSettings
        [HttpGet]
        public ActionResult<IEnumerable<FeatureSettings>> Get()
        {
            var featureSettings = _context.FeatureSettings.ToList();
            return featureSettings;
        }

        // GET api/featureSettings/{id}
        [HttpGet("{id}")]
        public ActionResult<FeatureSettings> Get(int id)
        {
            var featureSetting = _context.FeatureSettings.FirstOrDefault(fs => fs.Id == id);
            if (featureSetting == null)
            {
                return NotFound();
            }
            return featureSetting;
        }

        // POST api/featureSettings
        [HttpPost]
        public ActionResult<FeatureSettings> Post(FeatureSettings featureSetting)
        {
            _context.FeatureSettings.Add(featureSetting);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = featureSetting.Id }, featureSetting);
        }

        // PUT api/featureSettings/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, FeatureSettings featureSetting)
        {
            var existingFeatureSetting = _context.FeatureSettings.FirstOrDefault(fs => fs.Id == id);
            if (existingFeatureSetting == null)
            {
                return NotFound();
            }
            existingFeatureSetting.IsFeatureEnabled = featureSetting.IsFeatureEnabled;
            existingFeatureSetting.FeatureName = featureSetting.FeatureName;
            existingFeatureSetting.FeatureDescription = featureSetting.FeatureDescription;
            existingFeatureSetting.DisplayOrder = featureSetting.DisplayOrder;
            existingFeatureSetting.FeatureIcon = featureSetting.FeatureIcon;
            existingFeatureSetting.FeatureRoute = featureSetting.FeatureRoute;
            existingFeatureSetting.FeatureType = featureSetting.FeatureType;
            existingFeatureSetting.FeatureCategory = featureSetting.FeatureCategory;
            existingFeatureSetting.ParentFeatureId = featureSetting.ParentFeatureId;
            existingFeatureSetting.AdminFeature = featureSetting.AdminFeature;
            existingFeatureSetting.EnabledForRoles = featureSetting.EnabledForRoles;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/featureSettings/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var featureSetting = _context.FeatureSettings.FirstOrDefault(fs => fs.Id == id);
            if (featureSetting == null)
            {
                return NotFound();
            }
            _context.FeatureSettings.Remove(featureSetting);
            _context.SaveChanges();
            return NoContent();
        }
    }
}