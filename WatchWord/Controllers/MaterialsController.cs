using System;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Models;
using WatchWord.Service.Abstract;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class MaterialsController : Controller
    {
        private readonly IMaterialsService _service;
        public MaterialsController(IMaterialsService service) => _service = service;
        private static string DbError => "Database query error. Please try later.";

        [HttpGet]
        [Route("GetMaterials")]
        public async Task<string> GetMaterials(int page, int count)
        {
            var response = new MaterialsResponseModel() { Success = true };
            try
            {
                response.Materials = await _service.GetMaterials(page, count);
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
        [Route("GetCount")]
        public async Task<string> GetCount()
        {
            var response = new MaterialsCountResponseModel() { Success = true };
            try
            {
                response.Count = await _service.TotalCount();
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