using Microsoft.AspNetCore.Http;
using ImageProcessorCore;
using Abp.Authorization;

namespace WatchWord.Pictures
{
    public class PictureService : IPictureService
    {
        [AbpAuthorize("Member")]
        public string ParseImageFile(IFormFile file)
        {
            string base64;
            using (var stream = file.OpenReadStream())
            {
                var image = new Image(stream);
                base64 = image.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Crop,
                    Size = new Size { Width = 184, Height = 270 }
                }).ToString();
            }

            return base64;
        }
    }
}
