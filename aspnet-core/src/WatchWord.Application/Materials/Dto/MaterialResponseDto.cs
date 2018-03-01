using System.Collections.Generic;
using WatchWord.Domain.Entities;

namespace WatchWord.Materials.Dto
{
    public class MaterialResponseDto
    {
        public Material Material { get; set; }
        public IEnumerable<VocabWord> VocabWords { get; set; }
    }
}
