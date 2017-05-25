using System.IO;
using WatchWord.Service;
using WatchWord.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using WatchWord.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class ParseController : Controller
    {
        private readonly ScanWordParser _parser;

        public ParseController()
        {
            _parser = new ScanWordParser();
        }

        [HttpPost]
        [Authorize]
        [Route("File")]
        public string File(IFormFile file)
        {
            var responseModel = new ParseResponseModel();
            if (file.Length > 35000000)
            {
                responseModel.Succeeded = false;
                responseModel.Errors.Add("Subtitles file too big!");
            }
            else if (file.Length > 0)
            {
                var stream = file.OpenReadStream();
                var words = _parser.ParseUnigueWordsInFile(new Material(), new StreamReader(stream));

                if (words.Count > 0)
                {
                    responseModel.Succeeded = true;
                    responseModel.Words = words;
                }
                else
                {
                    responseModel.Succeeded = false;
                    responseModel.Errors.Add("Empty subtitles file!");
                }
            }
            else
            {
                responseModel.Succeeded = false;
                responseModel.Errors.Add("Empty subtitles file!");
            }

            return ApiJsonSerializer.Serialize(responseModel);
        }

        public class ParseResponseModel
        {
            public bool Succeeded { get; set; }
            public List<Word> Words { get; set; }
            public List<string> Errors { get; set; }

            public ParseResponseModel()
            {
                Words = new List<Word>();
                Errors = new List<string>();
            }
        }
    }
}