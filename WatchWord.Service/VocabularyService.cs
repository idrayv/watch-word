using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchWord.DataAccess.Abstract;
using WatchWord.Domain.Entity;
using WatchWord.Service.Abstract;

namespace WatchWord.Service
{
    public class VocabularyService : IVocabularyService
    {
        private readonly IWatchWordUnitOfWork _unitOfWork;
        private readonly IAccountsService _accountsService;
        private readonly IVocabWordsRepository _vocabWordsRepository;

        /// <summary>Initializes a new instance of the <see cref="VocabularyService"/> class.</summary>
        /// <param name="unitOfWork">Unit of work over WatchWord repositories.</param>
        /// <param name="accountsService">Accounts service.</param>
        public VocabularyService(IWatchWordUnitOfWork unitOfWork, IAccountsService accountsService)
        {
            _unitOfWork = unitOfWork;
            _accountsService = accountsService;
            _vocabWordsRepository = unitOfWork.Repository<IVocabWordsRepository>();
        }

        public async Task<List<VocabWord>> GetVocabWordsAsync(int userId)
        {
            var account = await _accountsService.GetByExternalIdAsync(userId);
            return await _vocabWordsRepository.GetAllAsync(v => v.Owner.Id == account.Id);
        }

        public async Task<int> InsertVocabWordAsync(VocabWord vocabWord, int userId)
        {
            var owner = await _accountsService.GetByExternalIdAsync(userId);
            vocabWord.Owner = owner;

            var existingVocabWord =
                await _vocabWordsRepository.GetByConditionAsync(
                    v => v.Word == vocabWord.Word && v.Owner.Id == vocabWord.Owner.Id, v => v.Owner);

            if (existingVocabWord != null)
            {
                existingVocabWord.Word = vocabWord.Word;
                existingVocabWord.Translation = vocabWord.Translation;
                existingVocabWord.Type = vocabWord.Type;
                _vocabWordsRepository.Update(existingVocabWord);
            }
            else
            {
                _vocabWordsRepository.Insert(vocabWord);
            }

            return await _unitOfWork.SaveAsync();
        }

        // TODO: return "unsigned words" instead of merge in js
        public async Task<List<VocabWord>> GetSpecifiedVocabWordsAsync(ICollection<Word> materialWords, int userId)
        {
            var arrayOfWords = materialWords == null
                ? new string[0]
                : materialWords.Select(n => n.TheWord).ToArray();

            var owner = await _accountsService.GetByExternalIdAsync(userId);
            var vocabWords =
                await _vocabWordsRepository.GetAllAsync(v => v.Owner.Id == owner.Id && arrayOfWords.Contains(v.Word));

            return vocabWords;
        }
    }
}