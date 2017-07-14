using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WatchWord.Models;
using WatchWord.Infrastructure;
using WatchWord.Domain.Identity;
using WatchWord.Service;
using WatchWord.Domain.Entity;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class ParseController : Controller
    {
        private IScanWordParser parser;
        private IVocabularyService vocabularyService;
        private UserManager<ApplicationUser> userManager;

        public ParseController(IScanWordParser parser, IVocabularyService vocabularyService, UserManager<ApplicationUser> userManager)
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
                    response.Success = false;
                    response.Errors = new List<string> { "Subtitles file too big!" };
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
                        Debug.Write(ex.ToString());
                        response.Errors.Add("Database query error. Please try later.");
                    }
                }
            }
            else
            {
                response.Errors.Add("Empty subtitles file!");
            }

            return ApiJsonSerializer.Serialize(response);
        }
    }
}