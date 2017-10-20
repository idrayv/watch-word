using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Infrastructure;
using WatchWord.Models;
using WatchWord.DataAccess.Identity;
using WatchWord.Service.Abstract;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : MainController
    {
        private readonly UserManager<WatchWordUser> _userManager;
        private readonly SignInManager<WatchWordUser> _signInManager;
        private readonly IAccountsService _accountsService;

        public AccountController(
            UserManager<WatchWordUser> userManager,
            SignInManager<WatchWordUser> signInManager,
            IAccountsService accountsService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountsService = accountsService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<string> Register([FromBody] AuthRequestModel authModel)
        {
            var registerModel = new AuthResponseModel();
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
                        registerModel.IsAdmin = true;
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Member");
                    }

                    await _signInManager.SignInAsync(user, false);

                    var aspUser = await _userManager.FindByNameAsync(authModel.Login);

                    if (aspUser != null)
                    {
                        registerModel.Account = await _accountsService.GetOrCreateAccountAsync(
                            aspUser.Id,
                            aspUser.UserName
                        );
                        registerModel.Success = true;
                    } else
                    {
                        registerModel.Errors.Add("User created but not logged in!");
                    }
                }
                else
                {
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
            var loginModel = new AuthResponseModel();
            try
            {
                var result = await _signInManager.PasswordSignInAsync(authModel.Login, authModel.Password, true, false);

                if (result.Succeeded)
                {
                    var aspUser = await _userManager.FindByNameAsync(authModel.Login);

                    if (aspUser != null)
                    {
                        loginModel.Account = await _accountsService.GetOrCreateAccountAsync(
                            aspUser.Id,
                            aspUser.UserName
                        );
                        loginModel.Success = true;
                        loginModel.IsAdmin = await _userManager.IsInRoleAsync(aspUser, "Admin");
                    }
                    else
                    {
                        loginModel.Errors.Add("User account does not exist. Please contact site administrator.");
                    }
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