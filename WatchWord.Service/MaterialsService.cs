using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.DataAccess.Repositories;
using WatchWord.Domain.Entity;
using WatchWord.Service.Abstract;
using WatchWord.DataAccess;
using WatchWord.DataAccess.Abstract;

namespace WatchWord.Service
{
    public class MaterialsService : IMaterialsService
    {
        private readonly IVocabularyService _vocabularyService;
        private readonly IMaterialsRepository _materialsRepository;
        private readonly IWordsRepository _wordsRepository;
        private readonly IWatchWordUnitOfWork _unitOfWork;

        // ReSharper disable once UnusedMember.Local
        /// <summary>Prevents a default instance of the <see cref="MaterialsService"/> class from being created.</summary>
        private MaterialsService()
        {
        }

        public MaterialsService(IWatchWordUnitOfWork unitOfWork, IVocabularyService vocabularyService)
        {
            _vocabularyService = vocabularyService;
            _wordsRepository = unitOfWork.Repository<IWordsRepository>();
            _materialsRepository = unitOfWork.Repository<IMaterialsRepository>();
            _unitOfWork = unitOfWork;
        }

        public async Task<Material> GetMaterial(int id)
        {
            return await _materialsRepository.GetByConditionAsync(m => m.Id == id, m => m.Words);
        }

        public async Task<List<VocabWord>> GetVocabWordsByMaterial(Material material, int userId)
        {
            return await _vocabularyService.GetSpecifiedVocabWordsAsync(material.Words, userId);
        }

        public async Task<IEnumerable<Material>> GetMaterials(int page, int count)
        {
            page = page < 1 ? 1 : page;
            return await _materialsRepository.SkipAndTakeAsync((page - 1) * count, count);
        }

        public async Task<int> SaveMaterial(Material material)
        {
            if (material.Id == 0)
            {
                _materialsRepository.Insert(material);
            }
            else
            {
                var words = await _wordsRepository.GetAllAsync(word =>
                    word.Material.Id == material.Id && material.Words.All(w => w.Id != word.Id));
                _wordsRepository.Delete(words);
                _materialsRepository.Update(material);
            }

            await _unitOfWork.SaveAsync();
            return material.Id;
        }

        public async Task<int> DeleteMaterial(int id)
        {
            _materialsRepository.Delete(id);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> TotalCount()
        {
            return await _materialsRepository.GetCount();
        }

        public async Task<List<Material>> GetMaterialsByPartialNameAsync(string partialName)
        {
            return await _materialsRepository.SkipAndTakeAsync(0, 8, m => m.Name.Contains(partialName));
        }
    }
}