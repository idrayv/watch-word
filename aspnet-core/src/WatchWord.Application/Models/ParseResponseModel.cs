using System.Collections.Generic;
using WatchWord.Domain.Entities;

namespace WatchWord.Models
{
    public class ParseResponseModel
    {
        public List<Word> Words { get; set; } = new List<Word>();
        public IEnumerable<VocabWord> VocabWords { get; set; } = new List<VocabWord>();
    }
}
