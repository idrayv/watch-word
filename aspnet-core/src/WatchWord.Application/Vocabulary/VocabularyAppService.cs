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
            var userId = AbpSession.UserId ?? 0;
            var result = await _vocabularyService.InsertVocabWordAsync(vocabWord, userId);

            if (result <= 0)
            {
                throw new UserFriendlyException("Word wasn't inserted into vocabulary!");
            }

            return result;
        }

        [AbpAuthorize("Member")]
        public async Task<List<VocabWord>> Get()
        {
            var userId = AbpSession.UserId ?? 0;
            return await _vocabularyService.GetVocabWordsAsync(userId);
        }
    }
}
