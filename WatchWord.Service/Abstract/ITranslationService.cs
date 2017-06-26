using System.Collections.Generic;
using System.Threading.Tasks;

namespace WatchWord.Service
{
    public interface ITranslationService
    {
        Task<List<string>> GetTranslations(string word);
    }
}