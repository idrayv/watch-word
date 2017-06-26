using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using ImageProcessorCore;

namespace WatchWord.Service
{
    public class PictureService : IPictureService
    {
        public string ParseImageFile(IFormFile file)
        {
            String base64;
            using (var stream = file.OpenReadStream())
            {
                MemoryStream ms = new MemoryStream();
                Image image = new Image(stream);
                base64 = image.Resize(new ResizeOptions()
                {
                    Mode = ResizeMode.Crop,
                    Size = new Size() { Width = 184, Height = 270 }
                }).ToString();
            }

            return base64;
        }
    }
}