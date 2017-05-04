using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WatchWord.Domain.Identity;
using WatchWord.DataAccess;
using WatchWord.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public ValuesController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // GET api/values
        [HttpGet]
        public async System.Threading.Tasks.Task<IEnumerable<string>> GetAsync()
        {
            var user = new ApplicationUser { UserName = "test", Email = "test@test.com" };
            var result = await _userManager.CreateAsync(user, "12345");

            using (var db = new WatchWordContext(_configuration))
            {
                var word = new Word { TheWord = "test" };
                db.Words.Add(word);
                db.SaveChanges();
            }

            if (result.Succeeded)
            {
                return new string[] { "Succeeded!" };
            } else
            {
                var errors = new List<string> { };
                foreach(var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return errors.ToArray();
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // POST api/values/AllowAnonymous
        [HttpGet]  
        [AllowAnonymous]
        [Route("AllowAnonymous")]
        public string AllowAnonymous()
        {
            return "AllowAnonymous";
        }

        // POST api/values/Authorize
        [HttpGet]
        [Authorize]
        [Route("Authorize")]
        public string Authorize()
        {
            return "Authorize";
        }
    }
}