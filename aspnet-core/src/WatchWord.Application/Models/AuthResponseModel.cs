using WatchWord.Domain.Entities;

namespace WatchWord.Models
{
    public class AuthResponseModel
    {
        public Account Account { get; set; }

        public bool IsAdmin { get; set; }
    }
}
