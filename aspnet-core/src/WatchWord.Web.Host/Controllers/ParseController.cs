using System.Threading.Tasks;
using System;
using System.IO;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Abp.UI;
using WatchWord.Controllers;
using WatchWord.Authorization.Users;
using WatchWord.Vocabulary;
using WatchWord.Web.Host.Infrastructure;
using WatchWord.Domain.Entities;
using WatchWord.ScanWord;
using WatchWord.Web.Host.Controllers.Dto;

namespace WatchWord.Web.Host.Controllers
{
    // TODO: Move to app service + add authorisation
    [Route("api/[controller]")]
    public class ParseController : WatchWordControllerBase
    {
        private readonly IScanWordParser _parser;
        private readonly IVocabularyService _vocabularyService;
        private readonly UserManager _userManager;

        public ParseController(IScanWordParser parser, IVocabularyService vocabularyService,
            UserManager userManager)
        {
            _parser = parser;
            _userManager = userManager;
            _vocabularyService = vocabularyService;
        }

        [HttpPost]
        [Route("File")]
        public async Task<string> FileAsync(IFormFile file)
        {
            if (file != null)
            {
                if (file.Length > 35000000)
                {
                    throw new UserFriendlyException("Subtitles file too big!");
                }
                else if (file.Length > 0)
                {
                    try
                    {
                        var stream = file.OpenReadStream();
                        var words = _parser.ParseUnigueWordsInFile(new Material(), new StreamReader(stream));
                        var userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id ?? 0;
                        var vocabWords = await _vocabularyService.GetSpecifiedVocabWordsAsync(words, userId);

                        if (words.Count > 0)
                        {
                            var response = new ParseResponseDto { Words = words, VocabWords = vocabWords };

                            return ApiJsonSerializer.Serialize(response);
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(ex.ToString());
                        throw new UserFriendlyException("Server error. Please try again later.");
                    }
                }
            }
            else
            {
                throw new UserFriendlyException("Empty subtitles file!");
            }

            throw new UserFriendlyException("Server error. Please try again later.");
        }
    }
}
