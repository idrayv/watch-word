using System;
using System.Diagnostics;
using System.Threading.Tasks;
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

        public async Task<SaveMaterialResponseDto> Save(Material material)
        {
            var response = new SaveMaterialResponseDto { };
            try
            {
                var userId = GetCurrentUserAsync()?.Id ?? 0;
                material.Owner = await _accountsService.GetByExternalIdAsync(userId);

                // TODO: Allow for admin
                var oldMaterial = await _materialsRepository.GetAllIncluding(m => m.Id == material.Id, m => m.Words, m => m.Owner).FirstOrDefaultAsync();
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
            }
            catch (Exception ex)
            {
                AddServerError(ex.Message);
            }

            return response;
        }

        public async Task Delete(long id)
        {
            try
            {
                await _materialsRepository.DeleteAsync(id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                AddServerError(ex.Message);
            }
        }

        public async Task<MaterialResponseDto> GetMaterial(long id)
        {
            var response = new MaterialResponseDto { };
            try
            {
                response.Material = await _materialsRepository.GetAllIncluding(m => m.Id == id, m => m.Words, m => m.Owner).FirstOrDefaultAsync();

                if (response.Material == null)
                {
                    throw new UserFriendlyException("Material with key " + id + " does not exist!");
                }
                else
                {
                    var userId = GetCurrentUserAsync()?.Id ?? 0;
                    response.VocabWords = await _vocabularyService.GetSpecifiedVocabWordsAsync(response.Material.Words, userId);
                }
            }
            catch (Exception ex)
            {
                AddServerError(ex.Message);
            }

            return response;
        }

        // TODO: move to static helper
        private void AddServerError(string message)
        {
            Trace.TraceError(message); // TODO: change to abp logging
            throw new UserFriendlyException("Server error. Please try again later.");
        }
    }
}
