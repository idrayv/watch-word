using Microsoft.AspNetCore.Antiforgery;
using WatchWord.Controllers;

namespace WatchWord.Web.Host.Controllers
{
    public class AntiForgeryController : WatchWordControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
