using System.Threading.Tasks;
using WatchWord.Domain.Entities;

namespace WatchWord.Accounts
{
    public interface IAccountsService
    {
        /// <summary>Gets user's account by external identifier.</summary>
        /// <param name="id">External identifier, asp.net identity id, etc.</param>
        /// <returns>User's account.</returns>
        Task<Account> GetByExternalIdAsync(long id);

        /// <summary>Gets  or creates user's account by external identifier and name.</summary>
        /// <param name="id">External identifier, asp.net identity id, etc.</param>
        /// <param name="name">External name.</param>
        /// <returns>User's account.</returns>
        Task<Account> GetOrCreateAccountAsync(long id, string name);
    }
}
