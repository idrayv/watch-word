using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Domain.Identity;
using WatchWord.Models;
using WatchWord.Service;
using WatchWord.Domain.Entity;
using WatchWord.Service.Abstract;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class VocabularyController : MainController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVocabularyService _vocabularyService;

        public VocabularyController(IVocabularyService vocabularyService, UserManager<ApplicationUser> userManager)
        {
            _vocabularyService = vocabularyService;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<string> Post([FromBody] VocabWord vocabWord)
        {
            var response = new BaseResponseModel { Success = true };
            try
            {
                var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
                var result = await _vocabularyService.InsertVocabWordAsync(vocabWord, userId);

                if (result <= 0)
                {
                    AddCustomError(response, "Word wasn't inserted into vocabulary!");
                }
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }

        [Authorize]
        [HttpGet]
        public async Task<string> Get()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var response = new VocabularyResponseModel { Success = true };
            try
            {
                response.VocabWords = await _vocabularyService.GetVocabWordsAsync(user?.Id ?? 0);
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }
    }
}