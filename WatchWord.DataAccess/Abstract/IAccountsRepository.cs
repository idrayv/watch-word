using WatchWord.DataAccess.Repositories;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Abstract
{
    public interface IAccountsRepository : IGenericRepository<Account, int>
    {
    }
}