using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
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
        Task<GeneralSettings> PostGeneralSettings([FromForm] GeneralSettingsDto generalSettingsDto);
        Task PutGeneralSettings(int id, [FromForm] GeneralSettingsDto generalSettingsDto);
        Task DeleteGeneralSettings(int id);
    }

    public class GeneralSettingsBL : IGeneralSettingsBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public GeneralSettingsBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task<GeneralSettings> GetGeneralSettings()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAppName()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralSettings> GetGeneralSettings(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralSettings> PostGeneralSettings([FromForm] GeneralSettingsDto generalSettingsDto)
        {
            throw new NotImplementedException();
        }

        public Task PutGeneralSettings(int id, [FromForm] GeneralSettingsDto generalSettingsDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGeneralSettings(int id)
        {
            throw new NotImplementedException();
        }
    }
}