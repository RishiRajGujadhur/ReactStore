using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface IGeneralSettingsBL
    {
        Task<GeneralSettings> GetGeneralSettings();
        Task<string> GetAppName();
        Task<GeneralSettings> GetGeneralSettings(int id);
        Task<(GeneralSettings, bool)> PostGeneralSettings([FromForm] GeneralSettingsDto generalSettingsDto, ClaimsPrincipal user);
        Task PutGeneralSettings(int id, [FromForm] GeneralSettingsDto generalSettingsDto, ClaimsPrincipal user);
        Task DeleteGeneralSettings(int id);
    }

    public class GeneralSettingsBL : IGeneralSettingsBL
    {
        private readonly ImageService _imageService;
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        public UserManager<User> _userManager { get; }

        public GeneralSettingsBL(StoreContext context, UserManager<User> userManager, ImageService imageService, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<GeneralSettings> GetGeneralSettings()
        {
            var generalSettings = await _context.GeneralSettings.FirstOrDefaultAsync();
            return generalSettings;
        }

        public async Task<string> GetAppName()
        {
            var generalSettings = await _context.GeneralSettings.FirstOrDefaultAsync();
            return generalSettings.AppName;
        }

        public async Task<GeneralSettings> GetGeneralSettings(int id)
        {
            var generalSettings = await _context.GeneralSettings.FindAsync(id);

            if (generalSettings == null)
            {
                return new GeneralSettings();
            }

            return generalSettings;
        }

        public async Task<(GeneralSettings, bool)> PostGeneralSettings([FromForm] GeneralSettingsDto generalSettingsDto, ClaimsPrincipal user)
        {
            if (generalSettingsDto.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(generalSettingsDto.File);

                if (imageResult.Error != null) throw new Exception(imageResult.Error.Message);

                generalSettingsDto.LogoURL = imageResult.SecureUrl.ToString();
                generalSettingsDto.PublicId = imageResult.PublicId;
            }

            var generalSettings = _mapper.Map<GeneralSettings>(generalSettingsDto);
            generalSettings.CreatedAtTimestamp = DateTime.Now;
            generalSettings.CreatedByUserName = user.Identity.Name;
            _context.GeneralSettings.Add(generalSettings);

            var result = await _context.SaveChangesAsync() > 0;
            return (generalSettings, result);

        }

        public async Task PutGeneralSettings(int id, [FromForm] GeneralSettingsDto generalSettingsDto, ClaimsPrincipal user)
        {
            if (id != generalSettingsDto.Id)
            {
                throw new Exception("Id mismatch");
            }
            
            if (generalSettingsDto.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(generalSettingsDto.File);

                if (imageResult.Error != null) throw new Exception(imageResult.Error.Message);

                generalSettingsDto.LogoURL = imageResult.SecureUrl.ToString();
                generalSettingsDto.PublicId = imageResult.PublicId;
            } 
            
            var generalSettings = _mapper.Map<GeneralSettings>(generalSettingsDto);
            generalSettings.LastModifiedTimestamp = DateTime.UtcNow;
            generalSettings.LastModifiedUserName = user.Identity.Name;
            _context.Entry(generalSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneralSettingsExists(id))
                {
                    throw new Exception("General Settings not found");
                }
                else
                {
                    throw;
                }
            } 
        }

        public async Task DeleteGeneralSettings(int id)
        {
            var generalSettings = await _context.GeneralSettings.FindAsync(id);
            if (generalSettings == null)
            {
                throw new Exception("General Settings not found");
            }

            _context.GeneralSettings.Remove(generalSettings);
            await _context.SaveChangesAsync(); 
        }

        #region Helper Methods
        private bool GeneralSettingsExists(int id)
        {
            return _context.GeneralSettings.Any(e => e.Id == id);
        }
        #endregion
    }
}