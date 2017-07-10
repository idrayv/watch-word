using System.Collections.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Models
{
    public class VocabularyResponseModel : BaseResponseModel
    {
        public List<VocabWord> VocabWords { get; set; }
    }
}