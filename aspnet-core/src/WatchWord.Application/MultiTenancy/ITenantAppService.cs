using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WatchWord.MultiTenancy.Dto;

namespace WatchWord.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
