using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Domain.Entity;
using WatchWord.Models;
using WatchWord.Service.Abstract;
using WatchWord.DataAccess.Identity;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : MainController
    {
        private readonly ISettingsService _settingsService;
        private readonly UserManager<WatchWordUser> _userManager;

        public SettingsController(ISettingsService settingsService, UserManager<WatchWordUser> userManager)
        {
            _settingsService = settingsService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        [Route("GetUnfilledSiteSettings")]
        public async Task<string> GetUnfilledSiteSettings()
        {
            var response = new SettingsResponseModel { Success = true };
            try
            {
                response.Settings = await _settingsService.GetUnfilledSiteSettings();
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }

        [HttpPost]
        [Authorize]
        [Route("InsertSiteSettings")]
        public async Task<string> InsertSiteSettings([FromBody] List<Setting> settings)
        {
            var response = new BaseResponseModel { Success = true };
            try
            {
                var userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id ?? 0;
                await _settingsService.InsertSiteSettings(settings, userId);
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }
    }
}