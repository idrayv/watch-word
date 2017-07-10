using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Domain.Entity;
using WatchWord.Models;
using WatchWord.Service.Abstract;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using WatchWord.Domain.Identity;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class MaterialController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMaterialsService materialService;
        private static string dbError = "Database query error. Please try later.";

        public MaterialController(IMaterialsService materialService, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.materialService = materialService;
        }

        [HttpPost]
        [Authorize]
        [Route("Save")]
        public async Task<string> Save([FromBody] Material material)
        {
            var response = new SaveMaterialResponseModel { Success = true };
            try
            {
                response.Id = await materialService.SaveMaterial(material);

                if (response.Id <= 0)
                {
                    response.Success = false;
                    response.Errors.Add(dbError);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                Debug.Write(ex.ToString());
                response.Errors.Add(dbError);
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
                if (await materialService.DeleteMaterial(id) <= 0)
                {
                    response.Success = false;
                    response.Errors.Add(dbError);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                Debug.Write(ex.ToString());
                response.Errors.Add(dbError);
            }

            return response.ToJson();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<string> GetMaterial(int id)
        {
            var response = new MaterialResponseModel() { Success = true };
            try
            {
                response.Material = await materialService.GetMaterial(id);

                if (response.Material == null)
                {
                    response.Success = false;
                    response.Errors.Add("Material with key " + id + " does not exist!");
                }
                else
                {
                    var userId = (await userManager.GetUserAsync(HttpContext.User)).Id;
                    response.VocabWords = await materialService.GetVocabWordsByMaterial(response.Material, userId);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                Debug.Write(ex.ToString());
                response.Errors.Add(dbError);
            }

            return response.ToJson();
        }
    }
}