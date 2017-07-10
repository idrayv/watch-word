using System.Collections.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Models
{
    public class MaterialResponseModel : BaseResponseModel
    {
        public Material Material { get; set; }
        public List<VocabWord> VocabWords { get; set; }
    }
}