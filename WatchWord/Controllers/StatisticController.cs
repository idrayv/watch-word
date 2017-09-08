using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Models;
using WatchWord.Service.Abstract;
using WatchWord.DataAccess.Identity;
using WatchWord.Domain.Entity;
using System.Linq;

namespace WatchWord.Controllers
{
    [Route("api/[controller]")]
    public class StatisticController : MainController
    {
        private readonly IStatisticService _statisticService;
        private readonly UserManager<WatchWordUser> _userManager;
        private const int count = 5;

        public StatisticController(IStatisticService statisticService, UserManager<WatchWordUser> userManager)
        {
            _statisticService = statisticService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("material/words/top")]
        public async Task<string> RandomMaterialTopWords()
        {
            var response = new RandomWordsResponseModel { Success = true };
            var userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id ?? 0;

            try
            {
                response.Material = (await _statisticService.GetRandomMaterialsAsync(1)).FirstOrDefault();
                if (response.Material != null)
                {
                    response.VocabWords = await _statisticService.GetTop(count, response.Material.Id, userId);
                }
                else
                {
                    response.VocabWords = new List<VocabWord>();
                }
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }

        [HttpGet]
        [Route("materials/random")]
        public async Task<string> GetRandomMaterials()
        {
            var response = new RandomMaterialsResponseModel { Success = true };
            try
            {
                response.Materials = await _statisticService.GetRandomMaterialsAsync(count);
            }
            catch (Exception ex)
            {
                AddServerError(response, ex);
            }

            return response.ToJson();
        }
    }
}