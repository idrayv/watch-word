using Microsoft.AspNetCore.Http;

namespace WatchWord.Service.Abstract
{
    public interface IPictureService
    {
        string ParseImageFile(IFormFile file);
    }
}