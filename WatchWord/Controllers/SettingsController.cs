using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Domain.Entity;
using WatchWord.Models;
using WatchWord.Service;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private readonly ISettingsService settingsService;
        private static string DbError => "Database query error. Please try later.";

        public SettingsController(ISettingsService settingsService) => this.settingsService = settingsService;

        [HttpGet]
        [Authorize]
        [Route("GetUnfilledSiteSettings")]
        public async Task<string> GetUnfilledSiteSettings()
        {
            var response = new SettingsResponseModel() { Success = true };
            try
            {
                response.Settings = await settingsService.GetUnfilledSiteSettings();
            }
            catch (Exception ex)
            {
                response.Success = false;
                Debug.Write(ex.ToString());
                response.Errors.Add(DbError);
            }

            return response.ToJson();
        }

        [HttpPost]
        [Authorize]
        [Route("InsertSiteSettings")]
        public async Task<string> InsertSiteSettings([FromBody] List<Setting> settings)
        {
            var response = new BaseResponseModel() { Success = true };
            try
            {
                await settingsService.InsertSiteSettings(settings);
            }
            catch (Exception ex)
            {
                response.Success = false;
                Debug.Write(ex.ToString());
                response.Errors.Add(DbError);
            }

            return response.ToJson();
        }
    }
}