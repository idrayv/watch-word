using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WatchWord.Service.Abstract;
using Microsoft.AspNetCore.Identity;
using WatchWord.DataAccess.Identity;
using WatchWord.Models;
using System.Threading.Tasks;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class FavoriteMaterialController : MainController
    {
        private readonly IAccountsService _accountsService;
        private readonly UserManager<WatchWordUser> _userManager;
        private readonly IFavoriteMaterialsService _favoriteMaterialsService;

        public FavoriteMaterialController(
            IAccountsService accountsService,
            UserManager<WatchWordUser> userManager,
            IFavoriteMaterialsService favoriteMaterialsService)
        {
            _accountsService = accountsService;
            _userManager = userManager;
            _favoriteMaterialsService = favoriteMaterialsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<string> Post(int materialId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var response = new BaseResponseModel { Success = true };

            try
            {
                if (currentUser == null)
                {
                    AddCustomError(response, "You are not alowed to do this operation.");
                    response.Success = false;
                }
                else
                {
                    var account = await _accountsService.GetByExternalIdAsync(currentUser.Id);

                    if (await _favoriteMaterialsService.AddAsync(account, materialId) != 1)
                    {
                        response.Success = false;
                        AddCustomError(response, "The material was not saved as a favorite.");
                    }
                }
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }

        [HttpGet]
        [Authorize]
        [Route("{materialId}")]
        public async Task<string> Get(int materialId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var response = new GetFavoriteMaterialResponseModel { Success = true };

            try
            {
                if (currentUser == null)
                {
                    AddCustomError(response, "You are not alowed to do this operation.");
                    response.Success = false;
                }
                else
                {
                    var account = await _accountsService.GetByExternalIdAsync(currentUser.Id);
                    response.IsFavorite = await _favoriteMaterialsService.IsFavoriteAsync(account, materialId);
                }
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }


        [Authorize]
        [HttpDelete]
        [Route("{materialId}")]
        public async Task<string> Delete(int materialId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var response = new BaseResponseModel { Success = true };

            try
            {
                if (currentUser == null)
                {
                    AddCustomError(response, "You are not alowed to do this operation.");
                    response.Success = false;
                }
                else
                {
                    var account = await _accountsService.GetByExternalIdAsync(currentUser.Id);

                    if (await _favoriteMaterialsService.DeleteAsync(account, materialId) != 1)
                    {
                        response.Success = false;
                        AddCustomError(response, "The material was not deleted from favorites.");
                    }
                }
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }
    }
}