using System.Collections.Generic;
using WatchWord.Domain.Entities;

namespace WatchWord.Models
{
    public class SettingsResponseModel
    {
        public List<WatchWordSetting> Settings { get; set; }
    }
}
