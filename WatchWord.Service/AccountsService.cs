using System.Threading.Tasks;
using WatchWord.DataAccess;
using WatchWord.DataAccess.Repositories;
using WatchWord.Domain.Entity;

namespace WatchWord.Service
{
    /// <summary>Represents a layer for work with user's accounts.</summary>
    public class AccountsService : IAccountsService
    {
        private readonly IWatchWordUnitOfWork unitOfWork;
        private IAccountsRepository accountsRepository;

        /// <summary>Prevents a default instance of the <see cref="AccountsService"/> class from being created.</summary>
        private AccountsService() { }

        /// <summary>Initializes a new instance of the <see cref="VocabularyService"/> class.</summary>
        /// <param name="watchWordUnitOfWork">Unit of work over WatchWord repositories.</param>
        public AccountsService(IWatchWordUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            accountsRepository = unitOfWork.Repository<IAccountsRepository>(); ;
        }

        public async Task<Account> GetByExternalId(int id)
        {
            var account = await accountsRepository.GetByConditionAsync(a => a.ExternalId == id);
            if (account != null) return account;
            var newAccount = new Account { ExternalId = id };
            accountsRepository.Insert(newAccount);
            await unitOfWork.SaveAsync();
            return newAccount;
        }
    }
}