using System.Threading.Tasks;
using WatchWord.DataAccess.Repositories;
using WatchWord.Domain.Entity;
using WatchWord.Service.Abstract;

namespace WatchWord.Service
{
    public class MaterialsService : IMaterialsService
    {
        private MaterialsRepository _repository;

        public MaterialsService(MaterialsRepository repository)
        {
            _repository = repository;
        }

        public Material GetMaterial(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<int> SaveMaterial(Material material)
        {
            _repository.Insert(material);
            return await _repository.SaveAsync();
        }

        public int TotalCount()
        {
            return _repository.GetCount();
        }
    }
}