using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Authorization.Users;
using WatchWord.Domain.Entities;

namespace WatchWord.Vocabulary
{
    public interface IVocabularyService
    {
        Task<List<VocabWord>> GetVocabWordsAsync(User account);
        Task<long> InsertVocabWordAsync(VocabWord vocabWord, User account);
        Task<IEnumerable<VocabWord>> GetSpecifiedVocabWordsAsync(ICollection<Word> materialWords, User account);
    }
}
