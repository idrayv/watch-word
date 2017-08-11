using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Domain.Entity;

namespace WatchWord.Service.Abstract
{
    public interface ISettingsService
    {
        /// <summary>Gets the site configuration settings, which is not yet filled.</summary>
        /// <returns>List of the site configuration settings.</returns>
        Task<List<Setting>> GetUnfilledSiteSettings();

        /// <summary>Inserts the site configuration settings to the data storage. Owner will not be filled.</summary>
        /// <param name="settings">List of the site configuration settings.</param>
        /// <returns>The count of changed elements in data storage.</returns>
        Task<int> InsertSiteSettings(List<Setting> settings);

        /// <summary>Gets the site configuration setting by its key.</summary>
        /// <param name="key">Setting key.</param>
        /// <returns>Setting entity.</returns>
        Task<Setting> GetSiteSettingAsync(SettingKey key);
    }
}