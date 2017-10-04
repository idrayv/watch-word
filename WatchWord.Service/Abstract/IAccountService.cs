using System.Threading.Tasks;
using WatchWord.Domain.Entity;

namespace WatchWord.Service.Abstract
{
    public interface IAccountsService
    {
        /// <summary>Gets user's account by external identifier.</summary>
        /// <param name="id">External identifier, asp.net identity id, etc.</param>
        /// <returns>User's account.</returns>
        Task<Account> GetByExternalIdAsync(int id);

        /// <summary>Gets  or creates user's account by external identifier and name.</summary>
        /// <param name="id">External identifier, asp.net identity id, etc.</param>
        /// <param name="name">External name.</param>
        /// <returns>User's account.</returns>
        Task<Account> GetOrCreateAccountAsync(int id, string name);
    }
}