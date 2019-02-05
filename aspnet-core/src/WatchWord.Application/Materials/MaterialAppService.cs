using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.Authorization;
using WatchWord.Entities;
using WatchWord.Materials.Dto;
using WatchWord.Vocabulary;
using WatchWord.Users.Dto;

namespace WatchWord.Materials
{
    public class MaterialAppService : WatchWordAppServiceBase, IMaterialAppService
    {
        readonly IRepository<Material, long> _materialsRepository;
        readonly IVocabularyService _vocabularyService;

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
                Material = await _materialsRepository.GetAll().Where(m => m.Id == id).Select(m => new MaterialDto
                {
                    Description = m.Description,
                    Id = m.Id,
                    Image = m.Image,
                    Words = m.Words.Select(w => new Word { Id = w.Id, Count = w.Count, TheWord = w.TheWord }).ToList(),
                    Name = m.Name,
                    Type = m.Type,
                    Owner = new UserDto { UserName = m.Owner.UserName, Id = m.OwnerId }

                }).FirstOrDefaultAsync()
            };

            if (response.Material == null)
            {
                throw new UserFriendlyException("Material with key " + id + " does not exist!");
            }

            var accountId = GetCurrentUserId();
            response.VocabWords = await _vocabularyService.GetSpecifiedVocabWordsAsync(id, accountId);

            return response;
        }

        public async Task<List<MaterialDto>> GetMaterials(int page, int count)
        {
            page = page < 1 ? 1 : page;
            return await _materialsRepository.GetAll()
            .Skip((page - 1) * count).Take(count)
            .Select(m => new MaterialDto {
                Name = m.Name,
                Image = m.Image,
                Id = m.Id
            })
            .ToListAsync();
        }

        public async Task<long> GetCount()
        {
            return await _materialsRepository.LongCountAsync();
        }

        public async Task<List<MaterialDto>> Search(string text)
        {
            return await _materialsRepository.GetAll()
            .Where(m => m.Name.Contains(text))
            .Select(m => new MaterialDto
            {
                Name = m.Name,
                Image = m.Image,
                Id = m.Id
            })
            .ToListAsync();
        }

        [AbpAuthorize("Member")]
        public async Task<SaveMaterialResponseDto> Save(MaterialDto materialDto)
        {
            var material = ObjectMapper.Map<Material>(materialDto);

            var response = new SaveMaterialResponseDto { };

            // TODO: Leave as is if current user is admin
            material.Owner = await GetCurrentUserAsync();

            // TODO: Allow for admin
            var isOtherOwner = _materialsRepository
                .GetAll()
                .Where(m => m.Id == material.Id && m.OwnerId != material.Owner.Id).Any();
            if (isOtherOwner)
            {
                throw new UserFriendlyException("You are not allowed to change other owner's materials!");
            }

            // TODO: Separate Insert and Update
            // TODO: Save only distinct words
            // TODO: Save total words count as Material field
            await _materialsRepository.InsertOrUpdateAsync(material);
            await CurrentUnitOfWork.SaveChangesAsync();

            response.Id = material.Id;
            if (response.Id <= 0)
            {
                throw new UserFriendlyException("Material wasn't saved to the database!");
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
