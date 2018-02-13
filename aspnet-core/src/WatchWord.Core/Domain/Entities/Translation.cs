using System;
using WatchWord.Domain.Entities.Common;

namespace WatchWord.Domain.Entities
{
    /// <summary>Word translation cache.</summary>
    public class Translation : Entity<int>
    {
        /// <summary>Gets or sets the original word.</summary>
        public string Word { get; set; }

        /// <summary>Gets or sets translation of the word.</summary>
        public string Translate { get; set; }

        /// <summary>Gets or sets the date when translation was added.</summary>
        public DateTime AddDate { get; set; }

        /// <summary>Gets or sets source of the translation.</summary>
        public TranslationSource Source { get; set; }
    }

    public enum TranslationSource
    {
        YandexTranslate,
        YandexDictionary
    }
}
