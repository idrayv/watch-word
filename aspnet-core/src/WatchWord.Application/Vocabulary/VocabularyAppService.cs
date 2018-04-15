using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.UI;
using WatchWord.Domain.Entities;

namespace WatchWord.Vocabulary
{
    public class VocabularyAppService : WatchWordAppServiceBase, IVocabularyAppService
    {
        private readonly IVocabularyService _vocabularyService;

        public VocabularyAppService(IVocabularyService vocabularyService)
        {
            _vocabularyService = vocabularyService;
        }

        [AbpAuthorize("Member")]
        public async Task<long> Post(VocabWord vocabWord)
        {
            var account = await GetCurrentUserOrNullAsync();
            var result = await _vocabularyService.InsertVocabWordAsync(vocabWord, account);

            if (result <= 0)
            {
                throw new UserFriendlyException("Word wasn't inserted into vocabulary!");
            }

            return result;
        }

        [AbpAuthorize("Member")]
        public async Task<List<VocabWord>> Get()
        {
            var account = await GetCurrentUserOrNullAsync();
            return await _vocabularyService.GetVocabWordsAsync(account);
        }
    }
}
