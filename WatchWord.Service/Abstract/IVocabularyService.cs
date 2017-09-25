using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Domain.Entity;

namespace WatchWord.Service.Abstract
{
    public interface IVocabularyService
    {
        Task<List<VocabWord>> GetVocabWordsAsync(int userId);
        Task<int> InsertVocabWordAsync(VocabWord vocabWord, int userId);
        Task<IEnumerable<VocabWord>> GetSpecifiedVocabWordsAsync(ICollection<Word> materialWords, int userId);
    }
}