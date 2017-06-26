using WatchWord.Infrastructure;
using WatchWord.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WatchWord.Service;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        IPictureService pictureService;

        public ImageController(IPictureService pictureService) => this.pictureService = pictureService;

        [HttpPost]
        [Authorize]
        [Route("Parse")]
        public string Parse(IFormFile file)
        {
            var responseModel = new ImageResponseModel { Success = false };
            responseModel.Errors.Add("Empty image file!");

            if (file != null)
            {
                if (file.Length > 35000000)
                {
                    responseModel.Success = false;
                    responseModel.Errors = new List<string> { "Image file too big!" };
                }
                else if (file.Length > 0)
                {
                    var base64 = pictureService.ParseImageFile(file);
                    if (base64.Length > 0)
                    {
                        responseModel.Success = true;
                        responseModel.Base64 = base64;
                    }
                }
            }

            return ApiJsonSerializer.Serialize(responseModel);
        }
    }
}