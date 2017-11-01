using System;
using System.Threading.Tasks;
using WatchWord.DataAccess.Abstract;
using WatchWord.Domain.Entity;
using WatchWord.Service.Abstract;

namespace WatchWord.Service
{
    public class FavoriteMaterialsService : IFavoriteMaterialsService
    {
        private readonly IWatchWordUnitOfWork _unitOfWork;
        private readonly IFavoriteMaterialsRepository _favoriteMaterialsRepository;
        private readonly IMaterialsService _materialsService;
                        
        public FavoriteMaterialsService(IWatchWordUnitOfWork unitOfWork, IFavoriteMaterialsRepository favoriteMaterialsRepository, IMaterialsService materialsService)
        {
            _unitOfWork = unitOfWork;
            _materialsService = materialsService;
            _favoriteMaterialsRepository = favoriteMaterialsRepository;
        }

        public async Task<int> AddAsync(Account account, int materialId)
        {
            if (account == null)
            {
                throw new ArgumentException("Invalid account.");
            }

            var material = await _materialsService.GetMaterial(materialId);

            if (material == null)
            {
                throw new ArgumentException("Invalid material.");
            }

            var favoriteMaterial = new FavoriteMaterial { Account = account, Material = material };
            _favoriteMaterialsRepository.Insert(favoriteMaterial);

            return await _unitOfWork.CommitAsync();

        }

        public async Task<bool> IsFavoriteAsync(Account account, int materialId)
        {
            if (account == null)
            {
                throw new ArgumentException("Invalid account.");
            }

            var count = await _favoriteMaterialsRepository.GetCount(f => f.Material.Id == materialId && f.Account.Id == account.Id);
            return count == 1;
        }

        public async Task<int> DeleteAsync(Account account, int materialId)
        {
            if (account == null)
            {
                throw new ArgumentException("Invalid account.");
            }

            var favoriteMaterial = await _favoriteMaterialsRepository.GetByConditionAsync(f => f.Material.Id == materialId && f.Account.Id == account.Id);

            if(favoriteMaterial == null)
            {
                throw new ArgumentException("Ivalid account or material");
            }

            _favoriteMaterialsRepository.Delete(favoriteMaterial);

            return await _unitOfWork.CommitAsync();
        }
    }
}
