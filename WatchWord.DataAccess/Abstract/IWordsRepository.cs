using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    public interface IWordsRepository : IGenericRepository<Word, int>
    {
    }
}