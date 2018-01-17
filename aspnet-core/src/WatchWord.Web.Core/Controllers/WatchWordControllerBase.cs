using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace WatchWord.Controllers
{
    public abstract class WatchWordControllerBase: AbpController
    {
        protected WatchWordControllerBase()
        {
            LocalizationSourceName = WatchWordConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
