using System.Threading.Tasks;
using Abp.Application.Services;
using WatchWord.Sessions.Dto;

namespace WatchWord.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
