using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using WatchWord.Authorization.Users;
using WatchWord.MultiTenancy;

namespace WatchWord
{
    /// <summary>
    /// Derive application services from this class.
    /// </summary>
    public abstract class WatchWordAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected WatchWordAppServiceBase()
        {
            LocalizationSourceName = WatchWordConsts.LocalizationSourceName;
        }

        protected virtual long GetCurrentUserId()
        {
            try
            {
                return AbpSession.GetUserId();
            }
            catch
            {
                return 0;
            }
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual async Task<User> GetCurrentUserOrNullAsync()
        {
            try
            {
                return await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            }
            catch
            {
                return null;
            }
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
