using AutoMapper;
using Entities.TrackCoins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.TrackCoinServices;
namespace TrendTraderPro.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Policy = "CustomAdminPolicy")]
    [ApiController]
    public class TrackCoinController : Controller
    {
        private readonly ITrackCoinService _trackCoinService;

        public TrackCoinController(ITrackCoinService trackCoinService)
        {
            _trackCoinService = trackCoinService;
        }

        [HttpPost]
        public async Task<IActionResult> SetTrackCoin(string coinIdStr)
        {
            try
            {
                TrackCoinDTO trackCoinDTO = await _trackCoinService.SetTrackCoinAsync(coinIdStr);
                return StatusCode(StatusCodes.Status201Created, trackCoinDTO);
            }
            catch (Exception ex)
            {
                return BadRequest("TrackCoinController-SetTrackCoin Hata:" + ex.InnerException?.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> SetUnTrackCoinToLastRequest(string coinIdStr, long second)
        {
            try
            {
                TrackCoinDTO trackCoinDTO = await _trackCoinService.SetUnTrackCoinToLastRequestAsync(coinIdStr, second);
                return Ok(trackCoinDTO);
            }
            catch (Exception ex)
            {
                return BadRequest("TrackCoinController-SetUnTrackCoinToLastRequest Hata:" + ex.InnerException?.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrackCoins()
        {
            try
            {
                List<TrackCoinDTO> trackCoinDTOs = await _trackCoinService.GetAllTrackCoinsAsync();
                return Ok(trackCoinDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest("TrackCoinController-GetTrackCoins Hata:" + ex.InnerException?.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrackCoinsOnlyTracked()
        {
            try
            {
                List<TrackCoinDTO> trackCoinDTOs = await _trackCoinService.GetAllTrackCoinsOnlyTrackedAsync();
                return Ok(trackCoinDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest("TrackCoinController-GetAllTrackCoinsOnlyTracked Hata:" + ex.InnerException?.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTrackCoin(string coinIdStr)
        {
            try
            {
                TrackCoinDTO trackCoinDTO = await _trackCoinService.GetTrackCoinAsync(coinIdStr);
                return Ok(trackCoinDTO);
            }
            catch (Exception ex)
            {
                return BadRequest("TrackCoinController-GetTrackCoin Hata:" + ex.InnerException?.Message);
            }
        }
    }
}
