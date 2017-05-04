using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Domain.Identity;
using WatchWord.Infrastructure;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<string> Register([FromBody] AuthRequestModel authModel)
        {
            var user = new ApplicationUser { UserName = authModel.Login, Email = authModel.Email };
            var result = await _userManager.CreateAsync(user, authModel.Password);
            var registerModel = new AuthResponseModel();

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                registerModel.Succeeded = true;
            }
            else
            {
                registerModel.Succeeded = false;
                foreach (var error in result.Errors)
                {
                    registerModel.Errors.Add(error.Description);
                }
            }

            return ApiJsonSerializer.Serialize(registerModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<string> Login([FromBody] AuthRequestModel authModel)
        {
            var result = await _signInManager.PasswordSignInAsync(authModel.Login, authModel.Password, true, lockoutOnFailure: false);

            var loginModel = new AuthResponseModel();
            if (result.Succeeded)
            {
                loginModel.Succeeded = true;
            }
            else if (result.RequiresTwoFactor)
            {
                loginModel.Succeeded = false;
            }
            else if (result.IsLockedOut)
            {
                loginModel.Succeeded = false;
                loginModel.Errors.Add("User account locked out.");
            }
            else
            {
                loginModel.Succeeded = false;
                loginModel.Errors.Add("Invalid login attempt.");
            }

            return ApiJsonSerializer.Serialize(loginModel);
        }

        [HttpPost]
        [Authorize]
        [Route("Logout")]
        public string Logout()
        {
            var result = _signInManager.SignOutAsync();
            var logoutModel = new AuthResponseModel();

            if (result.IsCompleted)
            {
                logoutModel.Succeeded = true;
            }
            else
            {
                logoutModel.Succeeded = false;
                logoutModel.Errors.Add(result.Exception.ToString());
            }

            return ApiJsonSerializer.Serialize(logoutModel);
        }
    }

    public class AuthRequestModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class AuthResponseModel
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }

        public AuthResponseModel()
        {
            Errors = new List<string>();
        }
    }
}