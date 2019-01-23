using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp;
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

        public async Task<List<VocabWord>> GetVocabWordsAsync(User account)
        {
            var vocabWords = await _vocabWordsRepository.GetAll()
            .Where(v => v.Owner.Id == account.Id)
            .Select(v => new VocabWord {
                Id = v.Id,
                Translation = v.Translation,
                Type = v.Type,
                Word = v.Word
            }).ToListAsync();

            return vocabWords;
        }

        public async Task<long> InsertVocabWordAsync(VocabWord vocabWord, User account)
        {
            if (account != null)
            {
                vocabWord.Owner = account;

                var existingVocabWord = await _vocabWordsRepository.GetAll()
                    .Where(v => v.Word == vocabWord.Word && v.Owner.Id == vocabWord.Owner.Id)
                    .Include(v => v.Owner).FirstOrDefaultAsync();

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

                return vocabWord.Id;
            }

            return 0;
        }

        public async Task InsertVocabWordsAsync(List<VocabWord> vocabWords, User account)
        {
            if (account != null)
            {
                var vocabWordsWords = vocabWords.Select(w => w.Word).ToList();

                var existingVocabWords = await _vocabWordsRepository.GetAll()
                    .Where(v => vocabWordsWords.Contains(v.Word) && v.Owner.Id == account.Id)
                    .Include(v => v.Owner).ToListAsync();

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
                .Where(v => v.Owner.Id == ownerId && arrayOfWords.Contains(v.Word))
                .Select(v => new VocabWord {
                    Translation = v.Translation,
                    Word = v.Word,
                    Type = v.Type,
                    Id = v.Id
                }).ToListAsync();

            vocabWords.AddRange(arrayOfWords.Except(vocabWords.Select(n => n.Word))
                .Select(w => new VocabWord { Type = VocabType.UnsignedWord, Word = w }));

            return vocabWords.OrderBy(w => w.Type);
        }
    }
}
