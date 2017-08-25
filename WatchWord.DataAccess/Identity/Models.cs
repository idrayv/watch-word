using Microsoft.AspNetCore.Identity;

namespace WatchWord.DataAccess.Identity
{
    public class WatchWordUser : IdentityUser<int>
    {
        public WatchWordUser() : base()
        {
        }
    }

    public class WatchWordRole : IdentityRole<int>
    {
        public WatchWordRole() : base()
        {
        }

        public WatchWordRole(string roleName)
        {
            Name = roleName;
        }
    }
}