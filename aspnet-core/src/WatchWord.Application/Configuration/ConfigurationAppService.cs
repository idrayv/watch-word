using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using WatchWord.Configuration.Dto;

namespace WatchWord.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : WatchWordAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
