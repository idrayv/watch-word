using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Domain.Identity;
using WatchWord.Models;
using WatchWord.Service;
using System.Diagnostics;
using WatchWord.Domain.Entity;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class VocabularyController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IVocabularyService vocabularyService;
        private const string DbError = "Database query error. Please try later.";

        public VocabularyController(IVocabularyService vocabularyService, UserManager<ApplicationUser> userManager)
        {
            this.vocabularyService = vocabularyService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<string> Post([FromBody] VocabWord vocabWord)
        {
            var response = new BaseResponseModel {Success = true};
            try
            {
                var userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
                var result = await vocabularyService.InsertVocabWordAsync(vocabWord, userId);

                if (result <= 0)
                {
                    response.Success = false;
                    response.Errors.Add("Word was not inserted into vocabulary.");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                Debug.Write(ex.ToString());
                response.Errors.Add(DbError);
            }

            return response.ToJson();
        }

        [Authorize]
        [HttpGet]
        public async Task<string> Get()
        {
            var userId = (await userManager.GetUserAsync(HttpContext.User)).Id;

            var response = new VocabularyResponseModel {Success = true};
            try
            {
                response.VocabWords = await vocabularyService.GetVocabWordsAsync(userId);
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