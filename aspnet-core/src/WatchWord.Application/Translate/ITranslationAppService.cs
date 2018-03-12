using System.Collections.Generic;
using System.Threading.Tasks;

namespace WatchWord.Translate
{
    public interface ITranslationAppService
    {
        Task<List<string>> Translate(string word);
    }
}
