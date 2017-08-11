using Microsoft.EntityFrameworkCore;
using WatchWord.DataAccess.Abstract;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for accounts.</summary>
    public class AccountsRepository : GenericRepository<Account, int>, IAccountsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AccountsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public AccountsRepository(DbContext context) : base(context)
        {
        }
    }
}