using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Abp.Domain.Repositories;
using WatchWord.Infrastructure;
using WatchWord.Domain.Entities;

namespace WatchWord.Translate
{
    public class TranslationAppService : WatchWordAppServiceBase, ITranslationAppService
    {
        private readonly IRepository<Translation, long> _translationsRepository;
        private readonly IConfiguration _configuration;
        private readonly WatchWordProxy _proxy;

        public TranslationAppService(
            IConfiguration configuration,
            IRepository<Translation, long> translationsRepository,
            WatchWordProxy proxy)
        {
            _configuration = configuration;
            _proxy = proxy;
            _translationsRepository = translationsRepository;
        }

        public async Task<List<string>> Translate(string word)
        {
            var translations = new List<string>();

            // Cache
            translations.AddRange(await GetTranslateFromCache(word));
            if (translations.Count != 0)
            {
                return translations;
            }

            // Yandex dictionary
            var source = TranslationSource.YandexDictionary;
            translations.AddRange(await GetYandexDictionaryTranslations(word));
            if (translations.Count == 0)
            {
                // Yandex translate
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

            // Delete if exist
            var existing = await _translationsRepository.GetAll().Where(t => t.Word == word).ToListAsync();
            foreach (var translation in existing)
            {
                _translationsRepository.Delete(translation);
            }

            // Save translations
            var translationsCaches = translations.Select(translation => new Translation
            {
                Word = word,
                Translate = translation,
                AddDate = DateTime.Now,
                Source = source
            }).ToList();

            foreach (var translationsCache in translationsCaches)
            {
                await _translationsRepository.InsertAsync(translationsCache);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>Gets translations list from the cache for specified word.</summary>
        /// <param name="word">Specified word.</param>
        /// <returns>List of the translations.</returns>
        private async Task<IEnumerable<string>> GetTranslateFromCache(string word)
        {
            return (await _translationsRepository.GetAllListAsync(t => t.Word == word)).Select(s => s.Translate);
        }

        /// <summary>Gets translations list using yandex translate api for specified word.</summary>
        /// <param name="word">Specified word.</param>
        /// <returns>List of the translations.</returns>
        private async Task<IEnumerable<string>> GetYandexTranslateWord(string word)
        {
            var translations = new HashSet<string>();

            var address = string.Format(
                "https://translate.yandex.net/api/v1.5/tr.json/translate?key={0}&lang={1}&text={2}",
                Uri.EscapeDataString(GetYandexTranslateApiKey()),
                Uri.EscapeDataString("en-ru"),
                Uri.EscapeDataString(word));

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(address);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            if (_configuration["Proxy:Enabled"] == "True")
            {
                httpWebRequest.Proxy = _proxy;
            }

            var httpResponse = ((HttpWebResponse)await httpWebRequest.GetResponseAsync()).GetResponseStream();
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
                Uri.EscapeDataString(GetYandexDictionaryApiKey()),
                Uri.EscapeDataString("en-ru"),
                Uri.EscapeDataString(word));

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(address);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            if (_configuration["Proxy:Enabled"] == "True")
            {
                httpWebRequest.Proxy = _proxy;
            }

            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
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

        private string GetYandexDictionaryApiKey()
        {
            return _configuration["Translate:YandexDictionaryApiKey"];
        }

        private string GetYandexTranslateApiKey()
        {
            return _configuration["Translate:YandexTranslateApiKey"];
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
