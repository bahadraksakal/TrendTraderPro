using Entities.Coins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.CoinModels;
using Services.CoinPriceHistoryServices;
using Services.CoinServices;

namespace TrendTraderPro.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class CoinController : Controller
    {
        private readonly ICoinService _coinService;
        private readonly ICoinPriceHistoryService _coinPriceHistoryService;
        
        public CoinController(ICoinService coinService, ICoinPriceHistoryService coinPriceHistoryService)
        {
            _coinService = coinService;
            _coinPriceHistoryService = coinPriceHistoryService;
        }

        [HttpPost]
        public async Task<IActionResult> SetCoinPriceHistory(CoinIdModel coinIdModel)
        {
            try
            {
                await _coinPriceHistoryService.SetCoinPriceHistories(coinIdModel.CoinId ?? "");
                return Ok("SetCoinPriceHistories is Successed");

            }catch(Exception ex)
            {
                return BadRequest("CoinController-SetCoinPriceHistory Hata:" + ex.InnerException?.Message);
            }
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

        [HttpGet]
        public async Task<IActionResult> GetCoinAdviseById(int coinId)
        {
            try
            {
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("CoinController-GetCoinAdviseById Hata:" + ex.InnerException?.Message);
            }
        }
    }
}
