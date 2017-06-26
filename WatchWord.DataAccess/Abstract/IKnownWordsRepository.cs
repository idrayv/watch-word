using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    public interface IKnownWordsRepository : IGenericRepository<KnownWord, int>
    {
    }
}