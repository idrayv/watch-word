using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Models;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class MaterialsController : Controller
    {
        //private readonly IMaterialService _service;

        //public MaterialsController(IMaterialService service) => _service = service;

        [HttpPost]
        [Authorize]
        [Route("Create")]
        public string Create(MaterialRequestModel material)
        {
            return string.Empty;
        }
    }
}