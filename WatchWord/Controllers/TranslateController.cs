using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WatchWord.Models;
using WatchWord.Service.Abstract;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class TranslateController : MainController
    {
        private readonly ITranslationService _translationService;

        public TranslateController(ITranslationService translationService) => _translationService = translationService;

        [HttpGet]
        [Authorize]
        [Route("{word}")]
        public async Task<string> Translate(string word)
        {
            var response = new TranslationResponseModel { Success = true };
            try
            {
                response.Translations = await _translationService.GetTranslations(word);
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }
    }
}