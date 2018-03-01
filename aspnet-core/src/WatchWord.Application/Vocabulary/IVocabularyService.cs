using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Domain.Entities;

namespace WatchWord.Vocabulary
{
    public interface IVocabularyService
    {
        Task<List<VocabWord>> GetVocabWordsAsync(long userId);
        Task<long> InsertVocabWordAsync(VocabWord vocabWord, long userId);
        Task<IEnumerable<VocabWord>> GetSpecifiedVocabWordsAsync(ICollection<Word> materialWords, long userId);
    }
}
