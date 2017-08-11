using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Domain.Entity;
using WatchWord.Models;
using WatchWord.Service.Abstract;
using WatchWord.Domain.Identity;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class MaterialController : MainController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMaterialsService _materialService;

        public MaterialController(IMaterialsService materialService, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _materialService = materialService;
        }

        [HttpPost]
        [Authorize]
        [Route("Save")]
        public async Task<string> Save([FromBody] Material material)
        {
            var response = new SaveMaterialResponseModel { Success = true };
            try
            {
                response.Id = await _materialService.SaveMaterial(material);

                if (response.Id <= 0)
                {
                    AddCustomError(response, "Material wasn't saved to the database!");
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
                if (await _materialService.DeleteMaterial(id) <= 0)
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
                response.Material = await _materialService.GetMaterial(id);

                if (response.Material == null)
                {
                    response.Success = false;
                    response.Errors.Add("Material with key " + id + " does not exist!");
                }
                else
                {
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    response.VocabWords =
                        await _materialService.GetVocabWordsByMaterial(response.Material, user?.Id ?? 0);
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