using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WatchWord.Domain.Entity;
using WatchWord.Models;
using WatchWord.Service.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class MaterialsController : Controller
    {
        private readonly IMaterialsService _service;

        public MaterialsController(IMaterialsService service) => _service = service;

        [HttpPost]
        [Authorize]
        [Route("Create")]
        public async Task<string> Create(MaterialRequestModel material)
        {
            BaseResponseModel response = new BaseResponseModel { Success = true };
            int rowsAffected;

            if (ControllerContext.ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var imageStream = material.Image.OpenReadStream())
                    {
                        imageStream.CopyTo(memoryStream);
                    }
                    rowsAffected = await _service.SaveMaterial(
                        new Material
                        {
                            Name = material.Name,
                            Description = material.Description,
                            MimeType = material.Image.ContentType,
                            Words = material.Words,
                            Type = material.Type,
                            Image = memoryStream.ToArray()
                        }
                    );
                    if (rowsAffected == 0)
                    {
                        response.Success = false;
                        response.Errors.Add("Material does not inserted");
                    }
                }
            }
            else
            {
                response.Success = false;
                response.Errors.AddRange(ControllerContext.ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            }

            return response.ToJson();
        }
    }
}