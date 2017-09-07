using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Domain.Entity;
using WatchWord.Models;
using WatchWord.Service.Abstract;
using WatchWord.DataAccess.Identity;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class MaterialController : MainController
    {
        private readonly UserManager<WatchWordUser> _userManager;
        private readonly IMaterialsService _materialsService;
        private readonly IVocabularyService _vocabularyService;
        private readonly IAccountsService _accountsService;

        public MaterialController(IMaterialsService materialService, IVocabularyService vocabularyService,
            IAccountsService accountsService, UserManager<WatchWordUser> userManager)
        {
            _materialsService = materialService;
            _vocabularyService = vocabularyService;
            _accountsService = accountsService;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        [Route("Save")]
        public async Task<string> Save([FromBody] Material material)
        {
            var response = new SaveMaterialResponseModel { Success = true };
            try
            {
                var userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id ?? 0;
                material.Owner = await _accountsService.GetByExternalIdAsync(userId);

                // TODO: Allow for admin
                var oldMaterial = await _materialsService.GetMaterial(material.Id);
                if (oldMaterial != null && oldMaterial.Owner.Id != material.Owner.Id)
                {
                    AddCustomError(response, "You are not allowed to change other owner's materials!");
                }
                else
                {
                    response.Id = await _materialsService.SaveMaterial(material);
                    if (response.Id <= 0)
                    {
                        AddCustomError(response, "Material wasn't saved to the database!");
                    }
                }
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<string> Delete(int id)
        {
            var response = new BaseResponseModel { Success = true };
            try
            {
                if (await _materialsService.DeleteMaterial(id) <= 0)
                {
                    AddCustomError(response, "Material wasn't deleted from database!");
                }
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<string> GetMaterial(int id)
        {
            var response = new MaterialResponseModel { Success = true };
            try
            {
                response.Material = await _materialsService.GetMaterial(id);

                if (response.Material == null)
                {
                    response.Success = false;
                    response.Errors.Add("Material with key " + id + " does not exist!");
                }
                else
                {
                    var userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id ?? 0;
                    response.VocabWords =
                        await _vocabularyService.GetSpecifiedVocabWordsAsync(response.Material.Words, userId);
                }
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }
    }
}