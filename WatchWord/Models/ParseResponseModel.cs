using System.Collections.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Models
{
    public class ParseResponseModel : BaseResponseModel
    {
        public List<Word> Words { get; set; } = new List<Word>();
        public List<VocabWord> VocabWords { get; set; } = new List<VocabWord>();
    }
}