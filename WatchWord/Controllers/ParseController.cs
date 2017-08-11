using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WatchWord.Models;
using WatchWord.Infrastructure;
using WatchWord.Domain.Identity;
using WatchWord.Domain.Entity;
using WatchWord.Service.Abstract;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class ParseController : MainController
    {
        private readonly IScanWordParser parser;
        private readonly IVocabularyService vocabularyService;
        private readonly UserManager<ApplicationUser> userManager;

        public ParseController(IScanWordParser parser, IVocabularyService vocabularyService,
            UserManager<ApplicationUser> userManager)
        {
            this.parser = parser;
            this.userManager = userManager;
            this.vocabularyService = vocabularyService;
        }

        [HttpPost]
        [Authorize]
        [Route("File")]
        public async Task<string> FileAsync(IFormFile file)
        {
            var response = new ParseResponseModel { Success = false };

            if (file != null)
            {
                if (file.Length > 35000000)
                {
                    AddCustomError(response, "Subtitles file too big!");
                }
                else if (file.Length > 0)
                {
                    try
                    {
                        var stream = file.OpenReadStream();
                        var words = parser.ParseUnigueWordsInFile(new Material(), new StreamReader(stream));
                        var userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
                        var vocabWords = await vocabularyService.GetSpecifiedVocabWordsAsync(words, userId);

                        if (words.Count > 0)
                        {
                            response.Success = true;
                            response.Words = words;
                            response.VocabWords = vocabWords;
                        }
                    }
                    catch (Exception ex)
                    {
                        AddServerError(response, ex);
                    }
                }
            }
            else
            {
                AddCustomError(response, "Empty subtitles file!");
            }

            return ApiJsonSerializer.Serialize(response);
        }
    }
}