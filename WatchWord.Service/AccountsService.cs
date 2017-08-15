using System.Threading.Tasks;
using WatchWord.DataAccess.Abstract;
using WatchWord.Domain.Entity;
using WatchWord.Service.Abstract;

namespace WatchWord.Service
{
    /// <summary>Represents a layer for work with user's accounts.</summary>
    public class AccountsService : IAccountsService
    {
        private readonly IWatchWordUnitOfWork _unitOfWork;
        private readonly IAccountsRepository _accountsRepository;

        /// <summary>Initializes a new instance of the <see cref="VocabularyService"/> class.</summary>
        /// <param name="unitOfWork">Unit of work over WatchWord repositories.</param>
        public AccountsService(IWatchWordUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _accountsRepository = unitOfWork.Repository<IAccountsRepository>();
        }

        public async Task<Account> GetByExternalIdAsync(int id)
        {
            var account = await _accountsRepository.GetByConditionAsync(a => a.ExternalId == id);
            if (account != null) return account;
            var newAccount = new Account { ExternalId = id };
            _accountsRepository.Insert(newAccount);
            await _unitOfWork.SaveAsync();
            return newAccount;
        }
    }
}