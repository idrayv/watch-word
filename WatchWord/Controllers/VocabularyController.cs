using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Domain.Identity;
using WatchWord.Models;
using WatchWord.Service;
using System.Diagnostics;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class VocabularyController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private readonly IVocabularyService vocabularyService;
        private static string DbError => "Database query error. Please try later.";

        public VocabularyController(IVocabularyService vocabularyService, UserManager<ApplicationUser> userManager)
        {
            this.vocabularyService = vocabularyService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<string> Post([FromBody]VocabularyRequestModel vocabularyRequestModel)
        {
            var response = new BaseResponseModel() { Success = true };
            try
            {
                int result = 0;
                var userId = (await userManager.GetUserAsync(HttpContext.User)).Id;

                if (vocabularyRequestModel.IsKnown)
                {
                    result = await vocabularyService.InsertKnownWord(vocabularyRequestModel.Word,
                        vocabularyRequestModel.Translation, userId);
                }
                else
                {
                    result = await vocabularyService.InsertLearnWord(vocabularyRequestModel.Word,
                        vocabularyRequestModel.Translation, userId);
                }

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
        public async Task<string> Get()
        {
            var userId = (await userManager.GetUserAsync(HttpContext.User)).Id;

            var response = new VocabularyResponseModel() { Success = true };
            try
            {
                response.KnownWords = await vocabularyService.GetKnownWords(userId);
                response.LearnWords = await vocabularyService.GetLearnWords(userId);
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