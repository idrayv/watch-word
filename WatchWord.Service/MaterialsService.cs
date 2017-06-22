using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using WatchWord.DataAccess.Repositories;
using WatchWord.Domain.Entity;
using WatchWord.Service.Abstract;

namespace WatchWord.Service
{
    public class MaterialsService : IMaterialsService
    {
        private MaterialsRepository _materialsRepository;
        private WordsRepository _wordsRepository;

        public MaterialsService(MaterialsRepository materialsRepository, WordsRepository wordsRepository)
        {
            _wordsRepository = wordsRepository;
            _materialsRepository = materialsRepository;
        }

        public async Task<Material> GetMaterial(int id)
        {
            return await _materialsRepository.GetByСondition(m => m.Id == id, m => m.Words);
        }

        public async Task<IEnumerable<Material>> GetMaterials(int page, int count)
        {
            page = page < 1 ? 1 : page;
            return await _materialsRepository.SkipAndTakeAsync((page - 1) * count, count, null, m => m.Words);
        }

        public async Task<int> SaveMaterial(Material material)
        {
            if (material.Id == 0)
            {
                _materialsRepository.Insert(material);
            }
            else
            {
                var words = await _wordsRepository.GetAllAsync(word => word.Material.Id == material.Id && !material.Words.Any(w => w.Id == word.Id));
                _wordsRepository.Delete(words);
                _materialsRepository.Update(material);
            }

            await _materialsRepository.SaveAsync();
            return material.Id;
        }

        public async Task<int> DeleteMaterial(int id)
        {
            _materialsRepository.Delete(id);
            return await _materialsRepository.SaveAsync();
        }

        public async Task<int> TotalCount()
        {
            return await _materialsRepository.GetCount();
        }
    }
}