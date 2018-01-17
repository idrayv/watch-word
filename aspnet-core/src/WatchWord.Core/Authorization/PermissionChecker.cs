using Abp.Authorization;
using WatchWord.Authorization.Roles;
using WatchWord.Authorization.Users;

namespace WatchWord.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
