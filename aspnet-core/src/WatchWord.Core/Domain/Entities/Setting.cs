using System;
using Abp.Domain.Entities;

namespace WatchWord.Domain.Entities
{
    public class WatchWordSetting : Entity<long>
    {
        public SettingKey Key { get; set; }
        public SettingType Type { get; set; }
        public virtual Account Owner { get; set; }
        public int Int { get; set; }
        public string String { get; set; }
        public bool Boolean { get; set; }
        public DateTime? Date { get; set; }
    }

    public enum SettingKey
    {
        YandexTranslateApiKey,
        YandexDictionaryApiKey
    }

    public enum SettingType
    {
        Int,
        String,
        Boolean,
        Date
    }
}
