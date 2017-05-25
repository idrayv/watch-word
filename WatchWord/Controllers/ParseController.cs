using System.IO;
using WatchWord.Service;
using WatchWord.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WatchWord.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using WatchWord.Models;

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
                responseModel.Success = false;
                responseModel.Errors.Add("Subtitles file too big!");
            }
            else if (file.Length > 0)
            {
                var stream = file.OpenReadStream();
                var words = _parser.ParseUnigueWordsInFile(new Material(), new StreamReader(stream));

                if (words.Count > 0)
                {
                    responseModel.Success = true;
                    responseModel.Words = words;
                }
                else
                {
                    responseModel.Success = false;
                    responseModel.Errors.Add("Empty subtitles file!");
                }
            }
            else
            {
                responseModel.Success = false;
                responseModel.Errors.Add("Empty subtitles file!");
            }

            return ApiJsonSerializer.Serialize(responseModel);
        }
    }
}