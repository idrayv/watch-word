using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using WatchWord.Domain.Entities;

namespace WatchWord.Accounts
{
    /// <summary>Represents a layer for work with user's accounts.</summary>
    public class AccountsService : WatchWordAppServiceBase, IAccountsService
    {
        private readonly IRepository<Account, long> _accountsRepository;

        /// <summary>Initializes a new instance of the <see cref="VocabularyService"/> class.</summary>
        public AccountsService(IRepository<Account, long> accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<Account> GetOrCreateAccountAsync(long id, string name)
        {
            var account = await _accountsRepository.GetAll().Where(a => a.ExternalId == id).FirstOrDefaultAsync();
            if (account != null) return account;
            var newAccount = new Account { ExternalId = id, Name = name };
            _accountsRepository.Insert(newAccount);
            await CurrentUnitOfWork.SaveChangesAsync();
            return newAccount;
        }

        public async Task<Account> GetByExternalIdAsync(long id)
        {
            return await _accountsRepository.GetAll().Where(a => a.ExternalId == id).FirstOrDefaultAsync();
        }
    }
}
