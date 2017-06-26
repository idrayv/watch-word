using System.Collections.Generic;

namespace WatchWord.Models
{
    public class TranslationResponseModel : BaseResponseModel
    {
        public List<string> Translations { get; set; }
    }
}