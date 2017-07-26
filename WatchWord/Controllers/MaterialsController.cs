using System;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Models;
using WatchWord.Service.Abstract;
using System.Diagnostics;
using System.Threading.Tasks;
using WatchWord.Domain.Entity;
using System.Linq;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class MaterialsController : Controller
    {
        private readonly IMaterialsService materialsService;
        private const string dbError = "Database query error. Please try later.";

        public MaterialsController(IMaterialsService materialsService) => this.materialsService = materialsService;

        [HttpGet]
        public async Task<string> Get(int page, int count)
        {
            var response = new EntitiesResponseModel<Material>() { Success = true };
            try
            {
                var materials = (await materialsService.GetMaterials(page, count)).ToList();
                response.Entities = materials;
            }
            catch (Exception ex)
            {
                AddErrors(response, ex);
            }

            return response.ToJson();
        }

        [HttpGet]
        [Route("GetCount")]
        public async Task<string> GetCount()
        {
            var response = new MaterialsCountResponseModel() { Success = true };
            try
            {
                response.Count = await materialsService.TotalCount();
            }
            catch (Exception ex)
            {
                AddErrors(response, ex);
            }

            return response.ToJson();
        }

        private void AddErrors(BaseResponseModel model, Exception ex)
        {
            model.Success = false;
            Debug.Write(ex.ToString());
            model.Errors.Add(dbError);
        }
    }
}