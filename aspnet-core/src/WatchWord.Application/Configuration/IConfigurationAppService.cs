using System.Threading.Tasks;
using WatchWord.Configuration.Dto;

namespace WatchWord.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
