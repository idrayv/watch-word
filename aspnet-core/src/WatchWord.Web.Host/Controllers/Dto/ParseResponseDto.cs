using System.Collections.Generic;
using WatchWord.Domain.Entities;

namespace WatchWord.Web.Host.Controllers.Dto
{
    public class ParseResponseDto
    {
        public List<Word> Words { get; set; } = new List<Word>();
        public IEnumerable<VocabWord> VocabWords { get; set; } = new List<VocabWord>();
    }
}
