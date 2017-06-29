using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.DataAccess.Repositories;
using WatchWord.Domain.Entity;
using WatchWord.Service.Abstract;
using WatchWord.DataAccess;

namespace WatchWord.Service
{
    public class MaterialsService : IMaterialsService
    {
        private IMaterialsRepository materialsRepository;
        private IWordsRepository wordsRepository;
        private IWatchWordUnitOfWork unitOfWork;

        /// <summary>Prevents a default instance of the <see cref="MaterialsService"/> class from being created.</summary>
        private MaterialsService() { }

        public MaterialsService(IWatchWordUnitOfWork unitOfWork)
        {
            this.wordsRepository = unitOfWork.Repository<IWordsRepository>();
            this.materialsRepository = unitOfWork.Repository<IMaterialsRepository>();
            this.unitOfWork = unitOfWork;
        }

        public async Task<Material> GetMaterial(int id)
        {
            return await materialsRepository.GetByConditionAsync(m => m.Id == id, m => m.Words);
        }

        public async Task<IEnumerable<Material>> GetMaterials(int page, int count)
        {
            page = page < 1 ? 1 : page;
            return await materialsRepository.SkipAndTakeAsync((page - 1) * count, count, null, m => m.Words);
        }

        public async Task<int> SaveMaterial(Material material)
        {
            if (material.Id == 0)
            {
                materialsRepository.Insert(material);
            }
            else
            {
                var words = await wordsRepository.GetAllAsync(word =>
                    word.Material.Id == material.Id && !material.Words.Any(w => w.Id == word.Id));
                wordsRepository.Delete(words);
                materialsRepository.Update(material);
            }

            await unitOfWork.SaveAsync();
            return material.Id;
        }

        public async Task<int> DeleteMaterial(int id)
        {
            materialsRepository.Delete(id);
            return await unitOfWork.SaveAsync();
        }

        public async Task<int> TotalCount()
        {
            return await materialsRepository.GetCount();
        }
    }
}