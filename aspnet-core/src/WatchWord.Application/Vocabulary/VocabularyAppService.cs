using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.UI;
using Abp.Authorization;
using WatchWord.Entities;

namespace WatchWord.Vocabulary
{
    public class VocabularyAppService : WatchWordAppServiceBase, IVocabularyAppService
    {
        private readonly IVocabularyService _vocabularyService;

        public VocabularyAppService(IVocabularyService vocabularyService)
        {
            _vocabularyService = vocabularyService;
        }

        #region CREATE

        [AbpAuthorize(AppConsts.Member)]
        public async Task<long> Post(VocabWord vocabWord)
        {
            var accountId = GetCurrentUserId();
            var result = await _vocabularyService.InsertVocabWordAsync(vocabWord, accountId);

            if (result <= 0)
            {
                throw new UserFriendlyException("Word wasn't inserted into vocabulary!");
            }

            return result;
        }

        #endregion

        #region READ

        [AbpAuthorize(AppConsts.Member)]
        public async Task<List<VocabWord>> Get()
        {
            var accountId = GetCurrentUserId();
            return await _vocabularyService.GetVocabWordsAsync(accountId);
        }

        [AbpAuthorize(AppConsts.Member)]
        public async Task<List<VocabWord>> GetKnownWords()
        {
            var accountId = GetCurrentUserId();
            return await _vocabularyService.GetKnownWordsAsync(accountId);
        }

        [AbpAuthorize(AppConsts.Member)]
        public async Task<List<LearnWord>> GetLearnWords()
        {
            var accountId = GetCurrentUserId();
            return await _vocabularyService.GetLearnWordsAsync(accountId);
        }

        #endregion

        #region UPDATE

        [AbpAuthorize(AppConsts.Member)]
        public async Task IncreaseCorrectGuessesCount(string word)
        {
            var accountId = GetCurrentUserId();
            await _vocabularyService.IncreaseCorrectGuessesCountAsync(word, accountId);
        }

        [AbpAuthorize(AppConsts.Member)]
        public async Task IncreaseWrongGuessesCount(string word)
        {
            var accountId = GetCurrentUserId();
            await _vocabularyService.IncreaseWrongGuessesCountAsync(word, accountId);
        }

        [AbpAuthorize(AppConsts.Member)]
        public async Task MarkAsKnown(List<string> words)
        {
            var accountId = GetCurrentUserId();
            await _vocabularyService.MarkWordsAsKnownAsync(words, accountId);
        }

        #endregion
    }
}
