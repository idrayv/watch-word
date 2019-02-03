using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Entities;

namespace WatchWord.Vocabulary
{
    public interface IVocabularyService
    {
    
        Task<long> InsertVocabWordAsync(VocabWord vocabWord, long accountId);

        Task<List<VocabWord>> GetVocabWordsAsync(long accountId);

        Task<List<VocabWord>> GetKnownWordsAsync(long accountId);

        Task<List<LearnWord>> GetLearnWordsAsync(long accountId);

        Task<IEnumerable<VocabWord>> GetSpecifiedVocabWordsAsync(ICollection<Word> materialWords, long accountId);

        Task IncreaseCorrectGuessesCountAsync(string word, long accountId);

        Task IncreaseWrongGuessesCountAsync(string word, long accountId);

        Task MarkWordsAsKnownAsync(List<string> words, long accountId);
    }
}
