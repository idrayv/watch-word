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

namespace WatchWord.Vocabulary
{
    public class VocabularyService : AbpServiceBase, IVocabularyService, ITransientDependency
    {
        private readonly IRepository<VocabWord, long> _vocabWordsRepository;
        private readonly IRepository<VocabWordStatistic, long> _vocabWordStatisticsRepository;

        /// <summary>Initializes a new instance of the <see cref="VocabularyService"/> class.</summary>
        /// <param name="vocabWordsRepository">Vocabulary words repository.</param>
        public VocabularyService(
            IRepository<VocabWord, long> vocabWordsRepository,
            IRepository<VocabWordStatistic, long> vocabWordStatisticsRepository)
        {
            _vocabWordsRepository = vocabWordsRepository;
            _vocabWordStatisticsRepository = vocabWordStatisticsRepository;
        }

        #region CREATE

        public async Task<long> InsertVocabWordAsync(VocabWord vocabWord, long accountId)
        {
            vocabWord.OwnerId = accountId;

            var existingVocabWord = await _vocabWordsRepository.GetAll()
                .Where(v => v.Word == vocabWord.Word && v.OwnerId == accountId)
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

        public async Task InsertVocabWordsAsync(List<VocabWord> vocabWords, long accountId)
        {
            var vocabWordsWords = vocabWords.Select(w => w.Word).ToList();

            var existingVocabWords = await _vocabWordsRepository.GetAll()
                .Where(v => vocabWordsWords.Contains(v.Word) && v.OwnerId == accountId)
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
                    vocabWord.OwnerId = accountId;
                    await _vocabWordsRepository.InsertAsync(vocabWord);
                }
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        #endregion

        #region READ

        public async Task<List<VocabWord>> GetVocabWordsAsync(long accountId)
        {
            var vocabWords = await _vocabWordsRepository.GetAll()
            .Where(v => v.OwnerId == accountId)
            .Select(v => SimplifyVocabWord(v)).ToListAsync();

            return vocabWords;
        }

        public async Task<List<VocabWord>> GetKnownWordsAsync(long accountId)
        {
            var vocabWords = await _vocabWordsRepository.GetAll()
            .Where(v => v.OwnerId == accountId && v.Type == VocabType.KnownWord)
            .Select(v => SimplifyVocabWord(v)).ToListAsync();

            return vocabWords;
        }

        public async Task<List<LearnWord>> GetLearnWordsAsync(long accountId)
        {
            var query = _vocabWordsRepository.GetAll()
                .Where(v => v.OwnerId == accountId && v.Type == VocabType.LearnWord)
                .GroupJoin(_vocabWordStatisticsRepository.GetAll(),
                    vocab => new { vocab.Word, vocab.OwnerId },
                    stats => new { stats.Word, stats.OwnerId },
                    (vocab, stats) => new { vocab, stats });

            var learnWordsQuery = query.SelectMany(e => e.stats.DefaultIfEmpty(), (e, stat) =>
                new LearnWord
                {
                    Id = e.vocab.Id,
                    Word = e.vocab.Word,
                    Translation = e.vocab.Translation,
                    CorrectGuessesCount = stat == null ? 0 : stat.CorrectGuesses,
                    WrongGuessesCount = stat == null ? 0 : stat.WrongGuesses
                });

            return await learnWordsQuery.ToListAsync();
        }

        // TODO: Optimize for SQL: use material id instead of words list
        public async Task<IEnumerable<VocabWord>> GetSpecifiedVocabWordsAsync(ICollection<Word> materialWords, long accountId)
        {
            var arrayOfWords = materialWords == null
                ? new string[0]
                : materialWords.Select(n => n.TheWord).ToArray();

            var vocabWords = await _vocabWordsRepository
                .GetAll()
                .Where(v => v.OwnerId == accountId && arrayOfWords.Contains(v.Word))
                .Select(v => SimplifyVocabWord(v)).ToListAsync();

            vocabWords.AddRange(arrayOfWords.Except(vocabWords.Select(n => n.Word))
                .Select(w => new VocabWord { Type = VocabType.UnsignedWord, Word = w }));

            return vocabWords.OrderBy(w => w.Type);
        }

        #endregion

        #region UPDATE

        public async Task IncreaseCorrectGuessesCountAsync(string word, long accountId)
        {
            var existingStats = await _vocabWordStatisticsRepository
                .FirstOrDefaultAsync(v => v.Word == word && v.OwnerId == accountId);

            if (existingStats == null)
            {
                await _vocabWordStatisticsRepository.InsertAsync(new VocabWordStatistic
                {
                    OwnerId = accountId,
                    Word = word,
                    CorrectGuesses = 1,
                    WrongGuesses = 0
                });
            }
            else
            {
                existingStats.CorrectGuesses += 1;
                await _vocabWordStatisticsRepository.UpdateAsync(existingStats);
            }
        }

        public async Task IncreaseWrongGuessesCountAsync(string word, long accountId)
        {
            var existingStats = await _vocabWordStatisticsRepository
                .FirstOrDefaultAsync(v => v.Word == word && v.OwnerId == accountId);

            if (existingStats == null)
            {
                await _vocabWordStatisticsRepository.InsertAsync(new VocabWordStatistic
                {
                    OwnerId = accountId,
                    Word = word,
                    CorrectGuesses = 0,
                    WrongGuesses = 1
                });
            }
            else
            {
                existingStats.WrongGuesses += 1;
                await _vocabWordStatisticsRepository.UpdateAsync(existingStats);
            }
        }

        public async Task MarkWordsAsKnownAsync(List<string> words, long accountId)
        {
            try
            {
                var existingVocabWords = await _vocabWordsRepository.GetAll()
                    .Where(v => words.Contains(v.Word) && v.OwnerId == accountId)
                    .ToListAsync();

                foreach (var existingVocabWord in existingVocabWords)
                {
                    existingVocabWord.Type = VocabType.KnownWord;
                    await _vocabWordsRepository.UpdateAsync(existingVocabWord);
                }

                var newWords = words.Where(w => !existingVocabWords.Any(v => v.Word == w));

                foreach (var word in newWords)
                {
                    await _vocabWordsRepository.InsertAsync(new VocabWord
                    {
                        OwnerId = accountId,
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

        #endregion

        #region PRIVATE

        private VocabWord SimplifyVocabWord(VocabWord v)
        {
            return new VocabWord
            {
                Id = v.Id,
                Translation = v.Translation,
                Type = v.Type,
                Word = v.Word
            };
        }

        #endregion
    }
}
