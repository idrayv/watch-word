using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WatchWord.Domain.Identity;
using WatchWord.Infrastructure;
using WatchWord.Models;

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
            var registerModel = new BaseResponseModel();

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                registerModel.Success = true;
            }
            else
            {
                registerModel.Success = false;
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

            var loginModel = new BaseResponseModel();
            if (result.Succeeded)
            {
                loginModel.Success = true;
            }
            else if (result.RequiresTwoFactor)
            {
                loginModel.Success = false;
            }
            else if (result.IsLockedOut)
            {
                loginModel.Success = false;
                loginModel.Errors.Add("User account locked out.");
            }
            else
            {
                loginModel.Success = false;
                loginModel.Errors.Add("Invalid login attempt.");
            }

            return loginModel.ToJson();
        }

        [HttpPost]
        [Route("Logout")]
        public string Logout()
        {
            var result = _signInManager.SignOutAsync();
            var logoutModel = new BaseResponseModel();

            if (result.IsCompleted)
            {
                logoutModel.Success = true;
            }
            else
            {
                logoutModel.Success = false;
                logoutModel.Errors.Add(result.Exception.ToString());
            }

            return ApiJsonSerializer.Serialize(logoutModel);
        }
    }
}