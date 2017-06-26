using Microsoft.AspNetCore.Http;

namespace WatchWord.Service
{
    public interface IPictureService
    {
        string ParseImageFile(IFormFile file);
    }
}