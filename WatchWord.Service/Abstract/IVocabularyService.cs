using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Domain.Entity;

namespace WatchWord.Service
{
    public interface IVocabularyService
    {
        /// <summary>Gets all words from user's vocabulary of known words.</summary>
        /// <param name="userId">External user's identifier.</param>
        /// <returns>List of words.</returns>
        Task<List<KnownWord>> GetKnownWords(int userId);

        /// <summary>Gets all words from user's vocabulary of learning words.</summary>
        /// <param name="userId">External user's identifier.</param>
        /// <returns>List of words.</returns>
        Task<List<LearnWord>> GetLearnWords(int userId);

        /// <summary>Inserts word to the user's vocabulary of known words.</summary>
        /// <param name="word">Original word.</param>
        /// <param name="translation">Translation of the word.</param>
        /// <param name="userId">External user's identifier.</param>
        /// <returns>The count of changed elements in data storage.</returns>
        Task<int> InsertKnownWord(string word, string translation, int userId);

        /// <summary>Inserts word to the user's vocabulary of learning words.</summary>
        /// <param name="word">Original word.</param>
        /// <param name="translation">Translation of the word.</param>
        /// <param name="userId">External user's identifier.</param>
        /// <returns>The count of changed elements in data storage.</returns>
        Task<int> InsertLearnWord(string word, string translation, int userId);

        /// <summary>Fills the list of words with their translations.</summary>
        /// <param name="words">Words without translation.</param>
        /// <param name="userId">External user's identifier.</param>
        /// <returns>List of original words with translations from owner's vocabulary.</returns>
        Task<List<VocabWord>> FillVocabByWords(string[] words, int userId);
    }
}