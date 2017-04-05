using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WatchWord.Domain.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
    }

    public class ApplicationRole : IdentityRole<int>
    {
    }
}