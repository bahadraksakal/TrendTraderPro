using AutoMapper;
using Entities.CoinPriceHistories;
using Microsoft.Extensions.Logging;
using Repositorys.CoinPriceHistoryRepository;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Services.CoinPriceHistoryServices
{
    public class CoinPriceHistoryService : ICoinPriceHistoryService 
    {
        private readonly ICoinPriceHistoriesRepository _coinPriceHistoryRepository;
        private readonly ILogger<ICoinPriceHistoryService> _logger;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public CoinPriceHistoryService(ICoinPriceHistoriesRepository coinPriceHistoryRepository, ILogger<ICoinPriceHistoryService> logger, IMapper mapper, HttpClient httpClient)
        {
            _coinPriceHistoryRepository = coinPriceHistoryRepository;
            _logger = logger;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public async Task SetCoinPriceHistories(string coinIdStr)
        {
            CoinPriceHistoryDTO coinPriceLastData = await _coinPriceHistoryRepository.GetCoinPricesHistoryLastData(coinIdStr);
            List<CoinPriceHistoryDTO> coinPriceHistoriesDTO;
            StringBuilder apiUrl = new StringBuilder($"https://api.coingecko.com/api/v3/coins/{coinIdStr}"); 

            if (coinPriceLastData == null)
            {
                apiUrl.Append("/market_chart?vs_currency=usd&days=max&interval=daily&precision=9");
            }
            else
            {
                DateTimeOffset coinPriceLastDate = new DateTimeOffset(coinPriceLastData.Date.GetValueOrDefault());
                string coinPriceLastDateTimestamp = coinPriceLastDate.ToUnixTimeSeconds().ToString();
                string currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                apiUrl.Append($"/market_chart/range?vs_currency=usd&from={coinPriceLastDateTimestamp}&to={currentTimestamp}&precision=9");
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl.ToString());

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    jsonContent = jsonContent.Replace("null", "-1");

                    coinPriceHistoriesDTO = CoinPriceHistoryGeckoApiDTODeserialize(jsonContent,coinIdStr) ;
                    if (coinPriceHistoriesDTO == null)
                    {
                        _logger.LogWarning($"CoinPriceHistoryService: [CoinPriceHistorys could not be get with CoinGecko - CoinStatusCode:{response.StatusCode} - Your coinId:{coinIdStr}]");
                        return;
                    }

                    coinPriceHistoriesDTO = _mapper.Map<List<CoinPriceHistoryDTO>>(coinPriceHistoriesDTO);
                    await _coinPriceHistoryRepository.SetCoinPriceHistorys(coinPriceHistoriesDTO);
                    return;
                }
                _logger.LogWarning($"CoinPriceHistoryService: [CoinPriceHistorys could not be get with CoinGecko - CoinStatusCode:{response.StatusCode}]");
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CoinPriceHistoryService SetCoinPriceHistories have an error: [Exception:{ex.Message} - InnerEx:{ex.InnerException?.Message}]");
                return;
            }
        }

        private List<CoinPriceHistoryDTO> CoinPriceHistoryGeckoApiDTODeserialize(string jsonContent,string coinIdStr)
        {
            CoinPriceHistoryGeckoApiDTO? coinPriceHistoryGeckoApiDTO = JsonSerializer.Deserialize<CoinPriceHistoryGeckoApiDTO>(jsonContent);
            List<CoinPriceHistoryDTO> coinPriceHistoriesDTO = new();
            for (int i = 0; i < coinPriceHistoryGeckoApiDTO?.prices?.Count; i++)
            {
                coinPriceHistoriesDTO.Add(new CoinPriceHistoryDTO
                {
                    CoinId = coinIdStr,
                    Price = coinPriceHistoryGeckoApiDTO?.prices?.ElementAt(i)[1],
                    MarketCap = coinPriceHistoryGeckoApiDTO?.market_caps?.ElementAt(i)[1],
                    TotalVolume = coinPriceHistoryGeckoApiDTO?.total_volumes?.ElementAt(i)[1],
                    Date = DateTimeOffset.FromUnixTimeMilliseconds((long)(coinPriceHistoryGeckoApiDTO?.prices?.ElementAt(i)[0]!)).DateTime,                    
                });
            }

            return coinPriceHistoriesDTO;
        }

        //get için esnek kullanım alanını test et.
    }
}
