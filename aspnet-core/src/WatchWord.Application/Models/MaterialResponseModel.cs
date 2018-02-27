using System.Collections.Generic;
using WatchWord.Domain.Entities;

namespace WatchWord.Models
{
    public class MaterialResponseModel
    {
        public Material Material { get; set; }
        public IEnumerable<VocabWord> VocabWords { get; set; }
    }
}
