using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WatchWord.Roles.Dto;
using WatchWord.Users.Dto;

namespace WatchWord.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
