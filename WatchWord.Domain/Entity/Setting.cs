using System;
using WatchWord.Domain.Entity.Common;

namespace WatchWord.Domain.Entity
{
    public class Setting : Entity<int>
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