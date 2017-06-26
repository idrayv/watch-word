using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
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
        [Route("GetUnfilledSiteSettings")]
        public async Task<string> GetUnfilledSiteSettings(int page, int count)
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