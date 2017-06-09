using System;
using System.IO;
using WatchWord.Infrastructure;
using WatchWord.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        [HttpPost]
        [Authorize]
        [Route("Parse")]
        public string Parse(IFormFile file)
        {
            // TODO: Service and Resize
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
                    var stream = file.OpenReadStream();
                    String base64;
                    using (var ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        base64 = Convert.ToBase64String(fileBytes);
                    }

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