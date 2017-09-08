using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.DataAccess.Abstract;
using WatchWord.Domain.Entity;
using WatchWord.Service.Abstract;

namespace WatchWord.Service
{
    public class StatisticService: IStatisticService
    {
        private readonly IVocabularyService _vocabularyService;
        private readonly IMaterialsRepository _materialsRepository;
        private readonly IWordsRepository _wordsRepository;
        private readonly IWatchWordUnitOfWork _unitOfWork;

        public StatisticService(IWatchWordUnitOfWork unitOfWork, IVocabularyService vocabularyService)
        {
            _vocabularyService = vocabularyService;
            _wordsRepository = unitOfWork.Repository<IWordsRepository>();
            _materialsRepository = unitOfWork.Repository<IMaterialsRepository>();
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Material>> GetRandomMaterialsAsync(int count)
        {
            return (await _materialsRepository.GetRandomEntititiesByConditionAsync(count)).ToList();
        }

        public async Task<List<VocabWord>> GetTop(int count, int materialId, int userId)
        {
            var words = await _wordsRepository.GetTopWordsByMaterialAsync(count, materialId);
            return await _vocabularyService.GetSpecifiedVocabWordsAsync(words, userId);
        }
    }
}