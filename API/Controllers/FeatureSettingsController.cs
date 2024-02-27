using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.DTOs;
using API.BL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeatureSettingsController : ControllerBase
    {
        private readonly IFeatureSettingsBL _featureSettingsBL;

        public FeatureSettingsController(IFeatureSettingsBL featureSettingsBL)
        {
            _featureSettingsBL = featureSettingsBL;
        }

        // GET api/featureSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeatureSettings>>> Get()
        {
            var featureSettings = await _featureSettingsBL.Get();
            return Ok(featureSettings);
        }

        // GET api/featureSettings/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FeatureSettings>> Get(int id)
        {
            var featureSetting = await _featureSettingsBL.Get(id);
            return Ok(featureSetting);
        }

        // POST api/featureSettings
        [HttpPost]
        public async Task<ActionResult<FeatureSettings>> Post(FeatureSettings featureSetting)
        {
            var newFeatureSetting = await _featureSettingsBL.Post(featureSetting);
            return CreatedAtAction(nameof(Get), new { id = newFeatureSetting.Id }, newFeatureSetting);
        }

        [HttpPut("{id}", Name = "ChangeStatus")]
        public async Task<IActionResult> SetFeatureStatus(int id, FeatureStatusDto status)
        {
            await _featureSettingsBL.SetFeatureStatus(id, status, User);
            return NoContent();
        }
    }
}