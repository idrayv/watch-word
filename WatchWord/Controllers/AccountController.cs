using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Infrastructure;
using WatchWord.Models;
using WatchWord.DataAccess.Identity;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : MainController
    {
        private readonly UserManager<WatchWordUser> _userManager;
        private readonly SignInManager<WatchWordUser> _signInManager;

        public AccountController(
            UserManager<WatchWordUser> userManager,
            SignInManager<WatchWordUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<string> Register([FromBody] AuthRequestModel authModel)
        {
            var registerModel = new BaseResponseModel();
            try
            {
                var user = new WatchWordUser { UserName = authModel.Login, Email = authModel.Email };
                var isFirstUser = _userManager.Users.FirstOrDefault() == null;
                var result = await _userManager.CreateAsync(user, authModel.Password);

                if (result.Succeeded)
                {
                    if (isFirstUser)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Member");
                    }

                    await _signInManager.SignInAsync(user, false);
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
            }
            catch (Exception ex)
            {
                AddServerError(registerModel, ex);
            }

            return ApiJsonSerializer.Serialize(registerModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<string> Login([FromBody] AuthRequestModel authModel)
        {
            var loginModel = new BaseResponseModel();
            try
            {
                var result = await _signInManager.PasswordSignInAsync(authModel.Login, authModel.Password, true, false);

                if (result.Succeeded)
                {
                    loginModel.Success = true;
                }

                else if (result.IsLockedOut)
                {
                    loginModel.Errors.Add("User account locked out.");
                }
                else
                {
                    loginModel.Errors.Add("Invalid login attempt.");
                }
            }
            catch (Exception ex)
            {
                AddServerError(loginModel, ex);
            }

            return loginModel.ToJson();
        }

        [HttpPost]
        [Route("Logout")]
        public string Logout()
        {
            var logoutModel = new BaseResponseModel();
            try
            {
                var result = _signInManager.SignOutAsync();

                if (result.IsCompleted)
                {
                    logoutModel.Success = true;
                }
                else
                {
                    logoutModel.Success = false;
                    logoutModel.Errors.Add(result.Exception.ToString());
                }
            }
            catch (Exception ex)
            {
                AddServerError(logoutModel, ex);
            }

            return ApiJsonSerializer.Serialize(logoutModel);
        }
    }
}