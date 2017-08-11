using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Abstract
{
    public interface IWordsRepository : IGenericRepository<Word, int>
    {
    }
}