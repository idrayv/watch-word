using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WatchWord.Models;
using WatchWord.Service;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class TranslateController : Controller
    {
        ITranslationService translationService;
        private static string DbError => "Database query error. Please try later.";

        public TranslateController(ITranslationService translationService) => this.translationService = translationService;

        [HttpGet]
        [Authorize]
        [Route("{word}")]
        public async Task<string> Translate(string word)
        {
            var response = new TranslationResponseModel() { Success = true };
            try
            {
                response.Translations = await translationService.GetTranslations(word);
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