using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Abp.UI;
using WatchWord.Accounts;
using WatchWord.Authorization.Users;
using WatchWord.Domain.Entities;
using WatchWord.Materials.Dto;
using WatchWord.Vocabulary;

namespace WatchWord.Materials
{
    public class MaterialAppService : WatchWordAppServiceBase, IMaterialAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<Material, long> _materialsRepository;
        private readonly IVocabularyService _vocabularyService;
        private readonly IAccountsService _accountsService;

        public MaterialAppService(IRepository<Material, long> materialsRepository, IVocabularyService vocabularyService,
            IAccountsService accountsService, UserManager userManager)
        {
            _materialsRepository = materialsRepository;
            _vocabularyService = vocabularyService;
            _accountsService = accountsService;
            _userManager = userManager;
        }

        public async Task<MaterialResponseDto> GetMaterial(long id)
        {
            var response = new MaterialResponseDto
            {
                // TODO: change to non-recursive Include()
                Material = await _materialsRepository.GetAll().Where(m => m.Id == id).Select(m => new Material
                {
                    Description = m.Description,
                    Id = m.Id,
                    Image = m.Image,
                    Words = m.Words.Select(w => new Word { Id = w.Id, Count = w.Count, TheWord = w.TheWord }).ToList(),
                    Name = m.Name,
                    Type = m.Type,
                    Owner = m.Owner

                }).FirstOrDefaultAsync()
            };

            if (response.Material == null)
            {
                throw new UserFriendlyException("Material with key " + id + " does not exist!");
            }
            else
            {
                var userId = GetCurrentUserAsync()?.Id ?? 0;
                response.VocabWords = await _vocabularyService.GetSpecifiedVocabWordsAsync(response.Material.Words, userId);
            }

            return response;
        }

        public async Task<List<Material>> GetMaterials(int page, int count)
        {
            page = page < 1 ? 1 : page;
            return await _materialsRepository.GetAll().Skip((page - 1) * count).Take(count).ToListAsync();
        }

        public async Task<long> GetCount()
        {
            return await _materialsRepository.LongCountAsync();
        }

        public async Task<List<Material>> Search(string text)
        {
            return await _materialsRepository.GetAll().Where(m => m.Name.Contains(text)).ToListAsync();
        }

        public async Task<SaveMaterialResponseDto> Save(Material material)
        {
            var response = new SaveMaterialResponseDto { };

            var userId = GetCurrentUserAsync()?.Id ?? 0;
            material.Owner = await _accountsService.GetByExternalIdAsync(userId);

            // TODO: Allow for admin
            var oldMaterial = await _materialsRepository.GetAll().Where(m => m.Id == material.Id).Include(m => m.Owner).FirstOrDefaultAsync();
            if (oldMaterial != null && oldMaterial.Owner.Id != material.Owner.Id)
            {
                throw new UserFriendlyException("You are not allowed to change other owner's materials!");
            }
            else
            {
                await _materialsRepository.InsertOrUpdateAsync(material);
                await CurrentUnitOfWork.SaveChangesAsync();

                response.Id = material.Id;
                if (response.Id <= 0)
                {
                    throw new UserFriendlyException("Material wasn't saved to the database!");
                }
            }

            return response;
        }

        public async Task Delete(long id)
        {
            await _materialsRepository.DeleteAsync(id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
