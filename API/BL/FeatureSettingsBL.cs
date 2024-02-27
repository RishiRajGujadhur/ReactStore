using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extentions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface IFeatureSettingsBL
    {
        Task<IEnumerable<FeatureSettings>> Get();
        Task<FeatureSettings> Get(int id);
        Task<FeatureSettings> Post(FeatureSettings featureSetting);
        Task SetFeatureStatus(int id, FeatureStatusDto status, ClaimsPrincipal user);
        Task Delete(int id);
    }

    public class FeatureSettingsBL : IFeatureSettingsBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }
        private readonly ILogger<FeatureSettingsBL> _logger;
        private readonly IMapper _mapper;
        public FeatureSettingsBL(StoreContext context, UserManager<User> userManager,
         IMapper mapper, ILogger<FeatureSettingsBL> logger)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<FeatureSettings>> Get()
        {
            var featureSettings = await _context.FeatureSettings.OrderByDescending(f => f.Id).ToListAsync();
            return featureSettings;
        }

        public async Task<FeatureSettings> Get(int id)
        {
            var featureSetting = await _context.FeatureSettings.FirstOrDefaultAsync(fs => fs.Id == id);
            if (featureSetting == null)
            {
                throw new KeyNotFoundException("Feature setting not found");
            }
            return featureSetting;
        }

        public async Task<FeatureSettings> Post(FeatureSettings featureSetting)
        {
            _context.FeatureSettings.Add(featureSetting);
            await _context.SaveChangesAsync();
            return featureSetting;
        }

        public async Task SetFeatureStatus(int id, FeatureStatusDto status, ClaimsPrincipal user)
        {
            try
            {
                var feature = await _context.FeatureSettings
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

                if (feature == null)
                {
                    throw new Exception("Feature setting not found");
                }

                feature.IsFeatureEnabled = status.IsFeatureEnabled;

                _context.Entry(feature).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, LoggerExtention.AddErrorDetails(ex, user.Identity.Name, "An error occurred while updating invoice settings."));
                //Return a 500 Internal Server Error status code
                throw new Exception("500: An error occurred while updating feature settings");
            }  
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}