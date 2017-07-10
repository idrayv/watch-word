using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchWord.DataAccess;
using WatchWord.DataAccess.Repositories;
using WatchWord.Domain.Entity;

namespace WatchWord.Service
{
    public class VocabularyService : IVocabularyService
    {
        private readonly IWatchWordUnitOfWork unitOfWork;
        private readonly IAccountsService accountsService;
        private readonly IKnownWordsRepository knownWordsRepository;
        private readonly ILearnWordsRepository learnWordsRepository;

        /// <summary>Prevents a default instance of the <see cref="VocabularyService"/> class from being created.</summary>
        private VocabularyService() { }

        /// <summary>Initializes a new instance of the <see cref="VocabularyService"/> class.</summary>
        /// <param name="watchWordUnitOfWork">Unit of work over WatchWord repositories.</param>
        /// <param name="accountsService">Accounts service.</param>
        public VocabularyService(IWatchWordUnitOfWork unitOfWork, IAccountsService accountsService)
        {
            this.unitOfWork = unitOfWork;
            this.accountsService = accountsService;
            knownWordsRepository = unitOfWork.Repository<IKnownWordsRepository>();
            learnWordsRepository = unitOfWork.Repository<ILearnWordsRepository>();
        }

        public async Task<List<KnownWord>> GetKnownWords(int userId)
        {
            var account = await accountsService.GetByExternalIdAsync(userId);
            return await knownWordsRepository.GetAllAsync(k => k.Owner.Id == account.Id);
        }

        public async Task<List<LearnWord>> GetLearnWords(int userId)
        {
            var account = await accountsService.GetByExternalIdAsync(userId);
            return await learnWordsRepository.GetAllAsync(l => l.Owner.Id == account.Id);
        }

        public async Task<int> InsertKnownWord(string word, string translation, int userId)
        {
            var owner = await accountsService.GetByExternalIdAsync(userId);
            var knownWord = new KnownWord { Word = word, Translation = translation, Owner = owner, Type = VocabType.KnownWord };

            var existingLearnWord = await learnWordsRepository.GetByConditionAsync(
                l => l.Owner.Id == owner.Id && l.Word == word, l => l.Owner);

            var existingKnownWord = await knownWordsRepository.GetByConditionAsync(
                k => k.Owner.Id == owner.Id && k.Word == word, k => k.Owner);

            if (existingLearnWord != null)
            {
                // Delete translation from learning words vocabulary if exist.
                learnWordsRepository.Delete(existingLearnWord);
            }

            if (existingKnownWord != null)
            {
                // Update translation if word exist.
                existingKnownWord.Translation = translation;
                knownWordsRepository.Update(existingKnownWord);
            }
            else
            {
                // Insert if this a new word.
                knownWordsRepository.Insert(knownWord);
            }

            return await unitOfWork.SaveAsync();
        }

        public async Task<int> InsertLearnWord(string word, string translation, int userId)
        {
            var owner = await accountsService.GetByExternalIdAsync(userId);
            var learnWord = new LearnWord { Word = word, Translation = translation, Owner = owner, Type = VocabType.LearnWord };

            var existingLearnWord = await learnWordsRepository.GetByConditionAsync(
                l => l.Owner.Id == owner.Id && l.Word == word, l => l.Owner);

            var existingKnownWord = await knownWordsRepository.GetByConditionAsync(
                k => k.Owner.Id == owner.Id && k.Word == word, k => k.Owner);

            if (existingKnownWord != null)
            {
                // Delete translation from known words vocabulary if exist.
                knownWordsRepository.Delete(existingKnownWord);
            }

            if (existingLearnWord != null)
            {
                // Update translation if word exist.
                existingLearnWord.Translation = translation;
                learnWordsRepository.Update(existingLearnWord);
            }
            else
            {
                // Insert if this a new word.
                learnWordsRepository.Insert(learnWord);
            }

            return await unitOfWork.SaveAsync();
        }

        public async Task<List<VocabWord>> FillVocabByWords(string[] words, int userId)
        {
            var vocabWords = new List<VocabWord>();
            var owner = await accountsService.GetByExternalIdAsync(userId);

            vocabWords.AddRange(await learnWordsRepository.GetAllAsync(l => l.Owner.Id == owner.Id && words.Contains(l.Word)));
            vocabWords.AddRange(await knownWordsRepository.GetAllAsync(k => k.Owner.Id == owner.Id && words.Contains(k.Word)));

            foreach (var word in words.Where(word => vocabWords.All(v => v.Word != word)))
            {
                vocabWords.Add(new VocabWord { Word = word, Translation = "" });
            }

            return vocabWords;
        }
    }
}