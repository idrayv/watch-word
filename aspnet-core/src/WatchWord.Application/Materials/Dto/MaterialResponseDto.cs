using System.Collections.Generic;
using WatchWord.Entities;

namespace WatchWord.Materials.Dto
{
    public class MaterialResponseDto
    {
        public MaterialDto Material { get; set; }
        public IEnumerable<VocabWord> VocabWords { get; set; }
    }
}
