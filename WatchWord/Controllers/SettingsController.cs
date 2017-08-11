using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Domain.Entity;
using WatchWord.Models;
using WatchWord.Service.Abstract;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : MainController
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService) => _settingsService = settingsService;

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
                await _settingsService.InsertSiteSettings(settings);
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }
    }
}