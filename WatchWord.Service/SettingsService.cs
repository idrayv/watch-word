using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Domain.Entity;
using WatchWord.DataAccess.Abstract;
using WatchWord.Service.Abstract;

namespace WatchWord.Service
{
    /// <summary>Represents a layer for work with settings.</summary>
    public class SettingsService : ISettingsService
    {
        private readonly IWatchWordUnitOfWork _unitOfWork;
        private readonly ISettingsRepository _settingsRepository;
        private readonly IAccountsService _accountsService;

        /// <summary>These keys contain the specified data types.</summary>
        private static readonly Dictionary<SettingKey, SettingType> SettingKeyToTypeMapping =
            new Dictionary<SettingKey, SettingType>
            {
                { SettingKey.YandexDictionaryApiKey, SettingType.String },
                { SettingKey.YandexTranslateApiKey, SettingType.String }
            };

        /// <summary>These keys are responsible for the configuration of the site.</summary>
        private static readonly List<SettingKey> SiteSettingsKeys = new List<SettingKey>
        {
            SettingKey.YandexDictionaryApiKey,
            SettingKey.YandexTranslateApiKey
        };

        /// <summary>Initializes a new instance of the <see cref="SettingsService"/> class.</summary>
        /// <param name="unitOfWork">Unit of work over WatchWord repositories.</param>
        public SettingsService(IWatchWordUnitOfWork unitOfWork, IAccountsService accountsService)
        {
            _unitOfWork = unitOfWork;
            _settingsRepository = unitOfWork.Repository<ISettingsRepository>();
            _accountsService = accountsService;
        }

        public async Task<List<Setting>> GetUnfilledSiteSettings()
        {
            var filledAdminSettings = await _settingsRepository.GetAllAsync(s => SiteSettingsKeys.Contains(s.Key));
            var unfilledAdminSettings = (from settingKey in SiteSettingsKeys
                where filledAdminSettings.All(s => s.Key != settingKey)
                select CreateNewEmptySettingByKey(settingKey)).ToList();

            return unfilledAdminSettings;
        }

        public async Task<int> InsertSiteSettings(List<Setting> settings, int userId)
        {
            //TODO: universal parser

            var owner = await _accountsService.GetByExternalIdAsync(userId);
            foreach (var setting in settings)
            {
                setting.Owner = owner;
                if (setting.Type == SettingType.String && !string.IsNullOrEmpty(setting.String))
                {
                    _settingsRepository.Insert(setting);
                }
            }

            return await _unitOfWork.CommitAsync();
        }

        public async Task<Setting> GetSiteSettingAsync(SettingKey key)
        {
            return await _settingsRepository.GetByConditionAsync(s => s.Key == key);
        }

        /// <summary>Creates new empty setting with the specified key.</summary>
        /// <param name="key">Setting key.</param>
        /// <returns>Setting entity.</returns>
        private static Setting CreateNewEmptySettingByKey(SettingKey key)
        {
            var newSetting = new Setting { Key = key };

            var mapping = SettingKeyToTypeMapping.First(m => m.Key == key);
            newSetting.Type = mapping.Value;

            return newSetting;
        }
    }
}