using System.Collections.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Models
{
    public class RandomWordsResponseModel: BaseResponseModel
    {
        public IEnumerable<VocabWord> VocabWords { get; set; }
        public Material Material { get; set; }
    }
}
