using System.Collections.Generic;
using WatchWord.Domain.Entities;

namespace WatchWord.Models
{
    public class RandomWordsResponseModel
    {
        public IEnumerable<VocabWord> VocabWords { get; set; }
        public Material Material { get; set; }
    }
}
