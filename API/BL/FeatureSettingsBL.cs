using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface IFeatureSettingsBL
    {
        Task<IEnumerable<FeatureSettings>> Get();
        Task<FeatureSettings> Get(int id);
        Task<FeatureSettings> Post(FeatureSettings featureSetting);
        Task SetFeatureStatus(int id, FeatureStatusDto status);
        Task Delete(int id);
    }

    public class FeatureSettingsBL : IFeatureSettingsBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public FeatureSettingsBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task<IEnumerable<FeatureSettings>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<FeatureSettings> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FeatureSettings> Post(FeatureSettings featureSetting)
        {
            throw new NotImplementedException();
        }

        public Task SetFeatureStatus(int id, FeatureStatusDto status)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}