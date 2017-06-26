using System.Collections.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Models
{
    public class VocabularyResponseModel : BaseResponseModel
    {
        public List<KnownWord> KnownWords { get; set; }
        public List<LearnWord> LearnWords { get; set; }
    }
}