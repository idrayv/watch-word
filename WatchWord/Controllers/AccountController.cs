using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Domain.Identity;

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
        [Route("register")]
        public async Task<string> Register(string login = "", string email = "", string password = "")
        {
            var user = new ApplicationUser { UserName = login, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            var registerModel = new AuthModel();

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
            return JsonConvert.SerializeObject(registerModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<string> Login(string login = "", string password = "")
        {
            var result = await _signInManager.PasswordSignInAsync(login, password, true, lockoutOnFailure: false);

            var loginModel = new AuthModel();
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

            return JsonConvert.SerializeObject(loginModel);
        }

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public string Logout()
        {
            var result = _signInManager.SignOutAsync();
            var logoutModel = new AuthModel();

            if (result.IsCompleted)
            {
                logoutModel.Succeeded = true;
            }
            else
            {
                logoutModel.Succeeded = false;
                logoutModel.Errors.Add(result.Exception.ToString());
            }

            return JsonConvert.SerializeObject(logoutModel);
        }
    }

    public class AuthModel
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }

        public AuthModel()
        {
            Errors = new List<string>();
        }
    }
}