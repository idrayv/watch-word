using System.Collections.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Models
{
    public class SettingsResponseModel : BaseResponseModel
    {
        public List<Setting> Settings { get; set; }
    }
}