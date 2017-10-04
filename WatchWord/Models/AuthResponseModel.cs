using WatchWord.Domain.Entity;

namespace WatchWord.Models
{
    public class AuthResponseModel : BaseResponseModel
    {
        public Account Account { get; set; }
    }
}