using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp;
using Abp.UI;
using Abp.Domain.Repositories;
using Abp.Dependency;
using WatchWord.Entities;
using WatchWord.Authorization.Users;

namespace WatchWord.Vocabulary
{
    public class VocabularyService : AbpServiceBase, IVocabularyService, ITransientDependency
    {
        private readonly IRepository<VocabWord, long> _vocabWordsRepository;

        /// <summary>Initializes a new instance of the <see cref="VocabularyService"/> class.</summary>
        /// <param name="vocabWordsRepository">Vocabulary words repository.</param>
        public VocabularyService(IRepository<VocabWord, long> vocabWordsRepository)
        {
            _vocabWordsRepository = vocabWordsRepository;
        }

        public async Task<List<VocabWord>> GetVocabWordsAsync(long accountId)
        {
            var vocabWords = await _vocabWordsRepository.GetAll()
            .Where(v => v.OwnerId == accountId)
            .Select(SimplifyVocabWord()).ToListAsync();

            return vocabWords;
        }

        public async Task<List<VocabWord>> GetKnownWordsAsync(long accountId)
        {
            var vocabWords = await _vocabWordsRepository.GetAll()
            .Where(v => v.OwnerId == accountId && v.Type == VocabType.KnownWord)
            .Select(SimplifyVocabWord()).ToListAsync();

            return vocabWords;
        }

        public async Task<List<VocabWord>> GetLearnWordsAsync(long accountId)
        {
            var vocabWords = await _vocabWordsRepository.GetAll()
            .Where(v => v.OwnerId == accountId && v.Type == VocabType.LearnWord)
            .Select(SimplifyVocabWord()).ToListAsync();

            return vocabWords;
        }

        public async Task<long> InsertVocabWordAsync(VocabWord vocabWord, User account)
        {
            if (account != null)
            {
                vocabWord.Owner = account;

                var existingVocabWord = await _vocabWordsRepository.GetAll()
                    .Where(v => v.Word == vocabWord.Word && v.OwnerId == account.Id)
                    .FirstOrDefaultAsync();

                if (existingVocabWord != null)
                {
                    existingVocabWord.Word = vocabWord.Word;
                    existingVocabWord.Translation = vocabWord.Translation;
                    existingVocabWord.Type = vocabWord.Type;

                    await _vocabWordsRepository.UpdateAsync(existingVocabWord);
                }
                else
                {
                    await _vocabWordsRepository.InsertAsync(vocabWord);
                }

                await CurrentUnitOfWork.SaveChangesAsync();

                if (existingVocabWord != null)
                {
                    return existingVocabWord.Id;
                }

                return vocabWord.Id;
            }

            return 0;
        }

        public async Task MarkWordsAsKnownAsync(List<string> words, User account)
        {
            if (account == null)
            {
                throw new UserFriendlyException("Please log in to the application!");
            }

            try
            {
                var existingVocabWords = await _vocabWordsRepository.GetAll()
                    .Where(v => words.Contains(v.Word) && v.OwnerId == account.Id)
                    .ToListAsync();

                foreach (var existingVocabWord in existingVocabWords)
                {
                    await _vocabWordsRepository.DeleteAsync(existingVocabWord);
                }

                foreach (var word in words)
                {
                    await _vocabWordsRepository.InsertAsync(new VocabWord
                    {
                        Owner = account,
                        Type = VocabType.KnownWord,
                        Translation = "",
                        Word = word
                    });
                }

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException("Words wasn't inserted into vocabulary!");
            }
        }

        public async Task InsertVocabWordsAsync(List<VocabWord> vocabWords, User account)
        {
            if (account != null)
            {
                var vocabWordsWords = vocabWords.Select(w => w.Word).ToList();

                var existingVocabWords = await _vocabWordsRepository.GetAll()
                    .Where(v => vocabWordsWords.Contains(v.Word) && v.OwnerId == account.Id)
                    .ToListAsync();

                foreach (var vocabWord in vocabWords)
                {
                    var existingVocabWord = existingVocabWords.FirstOrDefault(v => v.Word == vocabWord.Word);
                    if (existingVocabWord != null)
                    {
                        existingVocabWord.Word = vocabWord.Word;
                        existingVocabWord.Translation = vocabWord.Translation;
                        existingVocabWord.Type = vocabWord.Type;
                        await _vocabWordsRepository.UpdateAsync(existingVocabWord);
                    }
                    else
                    {
                        vocabWord.Owner = account;
                        await _vocabWordsRepository.InsertAsync(vocabWord);
                    }
                }

                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        // TODO: Optimize for SQL: use material id instead of words list
        public async Task<IEnumerable<VocabWord>> GetSpecifiedVocabWordsAsync(ICollection<Word> materialWords, User account)
        {
            var arrayOfWords = materialWords == null
                ? new string[0]
                : materialWords.Select(n => n.TheWord).ToArray();

            var ownerId = account?.Id ?? 0;
            var vocabWords = await _vocabWordsRepository
                .GetAll()
                .Where(v => v.OwnerId == ownerId && arrayOfWords.Contains(v.Word))
                .Select(SimplifyVocabWord()).ToListAsync();

            vocabWords.AddRange(arrayOfWords.Except(vocabWords.Select(n => n.Word))
                .Select(w => new VocabWord { Type = VocabType.UnsignedWord, Word = w }));

            return vocabWords.OrderBy(w => w.Type);
        }

        private Expression<Func<VocabWord, VocabWord>> SimplifyVocabWord()
        {
            return (v) => new VocabWord
            {
                Id = v.Id,
                Translation = v.Translation,
                Type = v.Type,
                Word = v.Word
            };
        }
    }
}
