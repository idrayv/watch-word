using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.Authorization;
using WatchWord.Authorization.Users;
using WatchWord.Domain.Entities;
using WatchWord.Materials.Dto;
using WatchWord.Vocabulary;

namespace WatchWord.Materials
{
    public class MaterialAppService : WatchWordAppServiceBase, IMaterialAppService
    {
        private readonly IRepository<Material, long> _materialsRepository;
        private readonly IVocabularyService _vocabularyService;

        public MaterialAppService(
            IRepository<Material, long> materialsRepository,
            IVocabularyService vocabularyService)
        {
            _materialsRepository = materialsRepository;
            _vocabularyService = vocabularyService;
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

            var account = await GetCurrentUserOrNullAsync();
            response.VocabWords = await _vocabularyService.GetSpecifiedVocabWordsAsync(response.Material.Words, account);

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

        [AbpAuthorize("Member")]
        public async Task<SaveMaterialResponseDto> Save(Material material)
        {
            var response = new SaveMaterialResponseDto { };
            material.Owner = await GetCurrentUserAsync();

            // TODO: Allow for admin
            var isOtherOwner = _materialsRepository
                .GetAll()
                .Include(m => m.Owner)
                .Where(m => m.Id == material.Id && m.Owner.Id != material.Owner.Id).Any();
            if (isOtherOwner)
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

        [AbpAuthorize("Member")]
        public async Task Delete(long id)
        {
            // TODO: Allow to only author and admin
            await _materialsRepository.DeleteAsync(id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
