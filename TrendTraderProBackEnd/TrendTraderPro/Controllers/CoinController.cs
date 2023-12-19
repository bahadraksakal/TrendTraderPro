using Entities.Coins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.CoinModels;
using Services.GeckoApiServices;

namespace TrendTraderPro.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class CoinController : Controller
    {
        private readonly ICoinService _coinService;
        
        public CoinController(ICoinService coinService)
        {
            _coinService = coinService;
        }

        [Authorize(Policy = "CustomAdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> SetCoins()
        {
            try
            {
                SetCoinResponseModel setCoinResponseModel = new();
                List<CoinDTO> addedCoins= await _coinService.SetCoins();
                setCoinResponseModel.Data = addedCoins;
                setCoinResponseModel.Message = "Coins added successfully.";
                return StatusCode(StatusCodes.Status201Created, setCoinResponseModel);
            }
            catch (Exception ex)
            {
                return BadRequest("CoinController-SetCoins Hata:"+ ex.InnerException?.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCoinIdByName(string coinName)
        {
            try
            {
                CoinDTO coin = await _coinService.GetCoinIdByNameAsync(coinName);
                if(coin == null)
                {
                    return NotFound("Coin not found.");
                }
                return Ok(coin);
            }
            catch (Exception ex)
            {
                return BadRequest("CoinController-GetCoinIdByName Hata:" + ex.InnerException?.Message);
            }
        }
    }
}
