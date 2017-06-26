using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Domain.Entity;
using WatchWord.Models;
using WatchWord.Service.Abstract;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class MaterialController : Controller
    {
        private readonly IMaterialsService materialService;
        private static string DbError => "Database query error. Please try later.";

        public MaterialController(IMaterialsService materialService) => this.materialService = materialService;

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
                    response.Errors.Add(DbError);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                Debug.Write(ex.ToString());
                response.Errors.Add(DbError);
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
                    response.Errors.Add(DbError);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                Debug.Write(ex.ToString());
                response.Errors.Add(DbError);
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
            }
            catch (Exception ex)
            {
                response.Success = false;
                Debug.Write(ex.ToString());
                response.Errors.Add(DbError);
            }

            return response.ToJson();
        }
    }
}