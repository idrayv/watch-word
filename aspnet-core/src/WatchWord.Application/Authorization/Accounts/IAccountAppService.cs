using System.Threading.Tasks;
using Abp.Application.Services;
using WatchWord.Authorization.Accounts.Dto;

namespace WatchWord.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);

        Task ChangePassword(ChangePasswordInput input);
    }
}
