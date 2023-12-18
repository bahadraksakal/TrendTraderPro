using AutoMapper;
using DbContexts.DbContextTrendTraderPro;
using Entities.Coins;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositorys.UserRepositorys;
using System.Text.Json;

namespace Repositorys.CoinRepository
{
    public class CoinRepository : ICoinRepository
    {
        private readonly TrendTraderProDbContext _trendTraderProDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ICoinRepository> _logger;

        public CoinRepository(TrendTraderProDbContext trendTraderProDbContext, IMapper mapper, ILogger<ICoinRepository> logger)
        {
            _trendTraderProDbContext = trendTraderProDbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CoinDTO> AddCoinAsync(string idStr, string coinName, string coinSymbol)
        {
            Coin newCoin = new Coin() { Id=idStr, Name = coinName, Symbol = coinSymbol };
            await _trendTraderProDbContext.Coins.AddAsync(newCoin);
            await _trendTraderProDbContext.SaveChangesAsync();
            _logger.LogInformation($"Coin Başarıyla Eklendi: [Id:{idStr} - Name:{newCoin.Name} - Symbol:{newCoin.Symbol}]");
            return _mapper.Map<CoinDTO>(newCoin);
        }

        public async Task<CoinDTO> GetCoinByNameAsync(string coinName)
        {
            Coin? coin = await _trendTraderProDbContext.Coins.AsNoTracking().FirstOrDefaultAsync(coin => coin.Name == coinName);
            return _mapper.Map<CoinDTO>(coin);
        }

        public async Task<List<CoinDTO>> GetAllCoinsAsync()
        {
            List<CoinDTO> coins = await _trendTraderProDbContext.Coins.AsNoTracking().Select(coin => _mapper.Map<CoinDTO>(coin)).ToListAsync();
            return coins;
        }

        public async Task<List<CoinDTO>> SetCoinsAsync(List<Coin> newApiCoins)
        {
            List<Coin> dbCoins = await _trendTraderProDbContext.Coins.AsNoTracking().ToListAsync();
            List<Coin> missingCoins = FindMissingCoins(newApiCoins, dbCoins);

            _trendTraderProDbContext.Coins.AddRange(missingCoins);
            await _trendTraderProDbContext.SaveChangesAsync();

            _logger.LogInformation($"Coinler Başarıyla Eklendi: [Added Coins Count:{missingCoins.Count}]");
            return _mapper.Map<List<CoinDTO>>(missingCoins);
        }
        private List<Coin> FindMissingCoins(List<Coin> apiCoins, List<Coin> dbCoins)
        {
            List<Coin> missingCoins = apiCoins.Where(apiCoin => !dbCoins.Any(dbCoin => dbCoin.Id == apiCoin.Id)).ToList();
            return missingCoins;
        }

        
    }
}
