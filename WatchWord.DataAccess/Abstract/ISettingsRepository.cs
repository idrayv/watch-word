using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Abstract
{
    public interface ISettingsRepository : IGenericRepository<Setting, int>
    {
    }
}