using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Abp.UI;
using Abp.Authorization;
using WatchWord.Controllers;
using WatchWord.Pictures;

namespace WatchWord.Web.Host.Controllers
{
    [AbpAuthorize("Member")]
    [Route("api/[controller]")]
    public class ImageController : WatchWordControllerBase
    {
        private readonly IPictureService _pictureService;

        public ImageController(IPictureService pictureService) => _pictureService = pictureService;

        [HttpPost]
        [Route("Parse")]
        public string Parse(IFormFile file)
        {
            
            if (file == null)
            {
                throw new UserFriendlyException("Empty image file!");
            }

            if (file.Length > 35000000)
            {
                throw new UserFriendlyException("Image file too big!");
            }

            if (file.Length > 0)
            {
                var base64 = _pictureService.ParseImageFile(file);
                if (base64.Length <= 0)
                {
                    throw new UserFriendlyException("Empty image file!");
                }

                return base64;
            }

            throw new UserFriendlyException("Server error. Please try again later.");
        }
    }
}
