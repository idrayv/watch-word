using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Models;
using WatchWord.Service.Abstract;
using WatchWord.Domain.Entity;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class MaterialsController : MainController
    {
        private readonly IMaterialsService materialsService;

        public MaterialsController(IMaterialsService materialsService) => this.materialsService = materialsService;

        [HttpGet]
        public async Task<string> Get(int page, int count)
        {
            var response = new EntitiesResponseModel<Material> { Success = true };
            try
            {
                var materials = (await materialsService.GetMaterials(page, count)).ToList();
                response.Entities = materials;
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }

        [HttpGet]
        [Route("GetCount")]
        public async Task<string> GetCount()
        {
            var response = new MaterialsCountResponseModel { Success = true };
            try
            {
                response.Count = await materialsService.TotalCount();
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }

        [HttpGet]
        [Route("Search/{text?}")]
        public async Task<string> Search(string text)
        {
            var response = new EntitiesResponseModel<Material> { Success = true };
            try
            {
                response.Entities = await materialsService.GetMaterialsByPartialNameAsync(text);
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }
    }
}