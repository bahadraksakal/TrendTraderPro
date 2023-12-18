using Entities.Coins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.CoinModels;
using Services.GeckoApiServices;

namespace TrendTraderPro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoinController : Controller
    {
        private readonly ICoinService _coinService;
        
        public CoinController(ICoinService coinService)
        {
            _coinService = coinService;
        }

       // [Authorize(Policy = "CustomAdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> SetCoins()
        {
            try
            {
                SetCoinResponseModel setCoinResponseModel = new();
                List<CoinDTO> addedCoins= await _coinService.SetCoins();
                setCoinResponseModel.Data = addedCoins;
                setCoinResponseModel.Message = "Coins added successfully.";
                return Ok(setCoinResponseModel);
            }
            catch (Exception ex)
            {
                return BadRequest("CoinController-SetCoins Hata:" + ex.InnerException?.Message);
            }
        }
    }
}
