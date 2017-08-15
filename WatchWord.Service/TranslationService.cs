using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WatchWord.Domain.Entity;
using WatchWord.Service.Infrastructure;
using Microsoft.Extensions.Configuration;
using WatchWord.DataAccess.Abstract;
using WatchWord.Service.Abstract;

namespace WatchWord.Service
{
    public class TranslationService : ITranslationService
    {
        private static string _yandexTranslateApiKey;
        private static string _yandexDictionaryApiKey;
        private readonly ISettingsService _settingsService;
        private readonly IWatchWordUnitOfWork _unitOfWork;
        private readonly ITranslationsRepository _translationsRepository;
        private readonly IConfiguration _configuration;
        private readonly WatchWordProxy _proxy;

        /// <summary>Initializes a new instance of the <see cref="TranslationService"/> class.</summary>
        /// <param name="settingsService">Settings service.</param>
        /// <param name="unitOfWork">Unit of work over WatchWord repositories.</param>
        /// <param name="configuration">Settings configuration.</param>
        /// <param name="proxy">Web proxy.</param>
        public TranslationService(ISettingsService settingsService,
            IWatchWordUnitOfWork unitOfWork,
            IConfiguration configuration,
            WatchWordProxy proxy)
        {
            _settingsService = settingsService;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _proxy = proxy;
            _translationsRepository = unitOfWork.Repository<ITranslationsRepository>();
        }

        public async Task<List<string>> GetTranslations(string word)
        {
            var translations = new List<string>();

            // cache
            translations.AddRange(await GetTranslateFromCache(word));
            if (translations.Count != 0)
            {
                return translations;
            }

            // yandex dictionary
            var source = TranslationSource.YandexDictionary;
            translations.AddRange(await GetYandexDictionaryTranslations(word));
            if (translations.Count == 0)
            {
                // yandex translate
                source = TranslationSource.YandexTranslate;
                translations.AddRange(await GetYandexTranslateWord(word));
            }

            await SaveTranslationsToCache(word, translations, source);
            return translations;
        }

        /// <summary>Saves translations to the cache.</summary>
        /// <param name="word">Original word.</param>
        /// <param name="translations">List of translations.</param>
        /// <param name="source">Source of the translation.</param>
        private async Task SaveTranslationsToCache(string word, IReadOnlyCollection<string> translations,
            TranslationSource source)
        {
            await SaveTranslationsToCacheAsync(word, translations, source);
        }

        private async Task SaveTranslationsToCacheAsync(string word, IReadOnlyCollection<string> translations,
            TranslationSource source)
        {
            if (translations.Count == 0) return;

            // delete if exist
            var existing = await _translationsRepository.GetAllAsync(t => t.Word == word);
            foreach (var translation in existing)
            {
                _translationsRepository.Delete(translation);
            }

            // save translations
            var translationsCache = translations.Select(translation => new Translation
            {
                Word = word,
                Translate = translation,
                AddDate = DateTime.Now,
                Source = source
            }).ToList();

            _translationsRepository.Insert(translationsCache);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>Gets translations list from the cache for specified word.</summary>
        /// <param name="word">Specified word.</param>
        /// <returns>List of the translations.</returns>
        private async Task<IEnumerable<string>> GetTranslateFromCache(string word)
        {
            return (await _translationsRepository.GetAllAsync(t => t.Word == word)).Select(s => s.Translate);
        }

        /// <summary>Gets translations list using yandex translate api for specified word.</summary>
        /// <param name="word">Specified word.</param>
        /// <returns>List of the translations.</returns>
        private async Task<IEnumerable<string>> GetYandexTranslateWord(string word)
        {
            var translations = new HashSet<string>();

            var address = string.Format(
                "https://translate.yandex.net/api/v1.5/tr.json/translate?key={0}&lang={1}&text={2}",
                Uri.EscapeDataString(await GetYandexTranslateApiKey()),
                Uri.EscapeDataString("en-ru"),
                Uri.EscapeDataString(word));

            var httpWebRequest = (HttpWebRequest) WebRequest.Create(address);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            if (_configuration["Proxy:Enabled"] == "True")
            {
                httpWebRequest.Proxy = _proxy;
            }

            var httpResponse = ((HttpWebResponse) await httpWebRequest.GetResponseAsync()).GetResponseStream();
            if (httpResponse == null) return translations;

            string text;
            using (var streamReader = new StreamReader(httpResponse))
            {
                text = streamReader.ReadToEnd();
            }

            var yandexTranslateWords = JsonConvert.DeserializeObject<YandexTranslateWords>(text);
            foreach (var translation in yandexTranslateWords.text)
            {
                translations.Add(translation.ToLower());
            }

            return translations;
        }

        /// <summary>Gets translations list using yandex dictionary api for specified word.</summary>
        /// <param name="word">Specified word.</param>
        /// <returns>List of the translations.</returns>
        private async Task<IEnumerable<string>> GetYandexDictionaryTranslations(string word)
        {
            var translations = new HashSet<string>();

            var address = string.Format(
                "https://dictionary.yandex.net/api/v1/dicservice.json/lookup?key={0}&lang={1}&text={2}",
                Uri.EscapeDataString(await GetYandexDictionaryApiKey()),
                Uri.EscapeDataString("en-ru"),
                Uri.EscapeDataString(word));

            var httpWebRequest = (HttpWebRequest) WebRequest.Create(address);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            if (_configuration["Proxy:Enabled"] == "True")
            {
                httpWebRequest.Proxy = _proxy;
            }

            var httpResponse = (HttpWebResponse) await httpWebRequest.GetResponseAsync();
            if (httpResponse == null) return translations;

            string text;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                text = streamReader.ReadToEnd();
            }

            var yandexDictionaryTranslateWords = JsonConvert.DeserializeObject<YandexDictionaryTranslateWords>(text);
            foreach (var translation in yandexDictionaryTranslateWords.def.SelectMany(partOfSpeech => partOfSpeech.tr))
            {
                translations.Add(translation.text.ToLower());
            }

            return translations;
        }

        /// <summary>Gets yandex dictionary api key from data storage.</summary>
        /// <returns>Yandex dictionary api key.</returns>
        private async Task<string> GetYandexDictionaryApiKey()
        {
            if (!string.IsNullOrEmpty(_yandexDictionaryApiKey)) return _yandexDictionaryApiKey;
            var setting = await _settingsService.GetSiteSettingAsync(SettingKey.YandexDictionaryApiKey);
            if (setting != null)
            {
                _yandexDictionaryApiKey = setting.String;
            }
            else
            {
                throw new Exception("Yandex dictionary api key not found.");
            }

            return _yandexDictionaryApiKey;
        }

        /// <summary>Gets yandex translate api key from data storage.</summary>
        /// <returns>Yandex translate api key.</returns>
        private async Task<string> GetYandexTranslateApiKey()
        {
            if (!string.IsNullOrEmpty(_yandexTranslateApiKey)) return _yandexTranslateApiKey;
            var setting = await _settingsService.GetSiteSettingAsync(SettingKey.YandexTranslateApiKey);
            if (setting != null)
            {
                _yandexTranslateApiKey = setting.String;
            }
            else
            {
                throw new Exception("Yandex translate api key not found.");
            }

            return _yandexTranslateApiKey;
        }

        #region Yandex Classes

        // ReSharper disable InconsistentNaming
        protected class Head
        {
        }

        protected class Tr2
        {
            public string text { get; set; }
        }

        protected class Ex
        {
            public string text { get; set; }
            public List<Tr2> tr { get; set; }
        }

        protected class Mean
        {
            public string text { get; set; }
        }

        protected class Tr
        {
            public string text { get; set; }
            public string pos { get; set; }
            public List<Ex> ex { get; set; }
            public List<Mean> mean { get; set; }
        }

        protected class Def
        {
            public string text { get; set; }
            public string pos { get; set; }
            public string ts { get; set; }
            public List<Tr> tr { get; set; }
        }

        protected class YandexDictionaryTranslateWords
        {
            public Head head { get; set; }
            public List<Def> def { get; set; }
        }

        public class YandexTranslateWords
        {
            public int code { get; set; }
            public string lang { get; set; }
            public List<string> text { get; set; }
        }

        #endregion
    }
}