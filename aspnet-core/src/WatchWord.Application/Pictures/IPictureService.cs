using Microsoft.AspNetCore.Http;

namespace WatchWord.Pictures
{
    public interface IPictureService
    {
        string ParseImageFile(IFormFile file);
    }
}
