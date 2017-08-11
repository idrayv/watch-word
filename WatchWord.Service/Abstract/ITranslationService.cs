using System.Collections.Generic;
using System.Threading.Tasks;

namespace WatchWord.Service.Abstract
{
    public interface ITranslationService
    {
        /// <summary>Gets the list of translations of the word by using cache, ya dict, or ya translate api.</summary>
        /// <param name="word">Specified word.</param>
        /// <returns>List of translations.</returns>
        Task<List<string>> GetTranslations(string word);
    }
}