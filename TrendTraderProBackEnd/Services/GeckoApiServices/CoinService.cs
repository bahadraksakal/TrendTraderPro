using AutoMapper;
using Entities.Coins;
using Microsoft.Extensions.Logging;
using Repositorys.CoinRepository;
using System.Text.Json;

namespace Services.GeckoApiServices
{
    public class CoinService : ICoinService
    {
        private readonly ICoinRepository _coinRepository;
        private readonly ILogger<CoinService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        public CoinService(ICoinRepository coinRepository, ILogger<CoinService> logger, HttpClient httpClient, IMapper mapper)
        {
            _coinRepository = coinRepository;
            _logger = logger;
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<List<CoinDTO>> SetCoins()
        {

            List<CoinDTO> addedCoins = new();
            try
            {
                string apiUrl = "https://api.coingecko.com/api/v3/coins/list";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    List<Coin>? newApiCoins = _mapper.Map<List<Coin>>(JsonSerializer.Deserialize<List<CoinGeckoApiDTO>>(jsonContent));
                    if(newApiCoins == null)
                    {
                        _logger.LogWarning($"CoinService: [Coins could not be get with CoinGecko - CoinStatusCode:{response.StatusCode}]");
                        return addedCoins;
                    }
                    addedCoins = await _coinRepository.SetCoinsAsync(newApiCoins);
                    return addedCoins;
                }
                _logger.LogWarning($"CoinService: [Coins could not be get with CoinGecko - CoinStatusCode:{response.StatusCode}]");
                return addedCoins;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CoinService SetCoin have an error: [Exception:{ex.Message} - InnerEx:{ex.InnerException?.Message}]");
                return addedCoins;
            }
        }
    }
}
