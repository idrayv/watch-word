using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using WatchWord.Accounts;
using WatchWord.Domain.Entities;

namespace WatchWord.Vocabulary
{
    public class VocabularyService : WatchWordAppServiceBase, IVocabularyService
    {
        private readonly IAccountsService _accountsService;
        private readonly IRepository<VocabWord, long> _vocabWordsRepository;

        /// <summary>Initializes a new instance of the <see cref="VocabularyService"/> class.</summary>
        /// <param name="accountsService">Accounts service.</param>
        public VocabularyService(IAccountsService accountsService, IRepository<VocabWord, long> vocabWordsRepository)
        {
            _accountsService = accountsService;
            _vocabWordsRepository = vocabWordsRepository;
        }

        public async Task<List<VocabWord>> GetVocabWordsAsync(long userId)
        {
            var account = await _accountsService.GetByExternalIdAsync(userId);
            return await _vocabWordsRepository.GetAllListAsync(v => v.Owner.Id == account.Id);
        }

        public async Task<long> InsertVocabWordAsync(VocabWord vocabWord, long userId)
        {
            var owner = await _accountsService.GetByExternalIdAsync(userId);
            vocabWord.Owner = owner;

            var existingVocabWord = _vocabWordsRepository.GetAll().Where(v => v.Word == vocabWord.Word && v.Owner.Id == vocabWord.Owner.Id).Include(v => v.Owner).FirstOrDefault();

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

           await CurrentUnitOfWork.SaveChangesAsync();
           return vocabWord.Id;
        }

        // TODO: Optimize for SQL: use material id instead of words list
        public async Task<IEnumerable<VocabWord>> GetSpecifiedVocabWordsAsync(ICollection<Word> materialWords, long userId)
        {
            var arrayOfWords = materialWords == null
                ? new string[0]
                : materialWords.Select(n => n.TheWord).ToArray();

            var ownerId = (await _accountsService.GetByExternalIdAsync(userId))?.Id;
            var vocabWords = await _vocabWordsRepository.GetAllListAsync(v => v.Owner.Id == ownerId && arrayOfWords.Contains(v.Word));

            vocabWords.AddRange(arrayOfWords.Except(vocabWords.Select(n => n.Word))
                .Select(w => new VocabWord { Type = VocabType.UnsignedWord, Word = w }));

            return vocabWords.OrderBy(w => w.Type);
        }
    }
}
