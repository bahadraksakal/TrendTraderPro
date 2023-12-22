using Services.CoinServices;

namespace TrendTraderPro.Jobs
{
    public class JobSetCoins
    {
        private readonly ICoinService _coinService;
        private readonly ILogger<JobSetCoins> _logger;
        public JobSetCoins(ICoinService coinService, ILogger<JobSetCoins> logger)
        {
            _coinService = coinService;
            _logger = logger;
        }
        public async Task SetCoinsExecute()
        {
            _logger.LogInformation("JobSetCoins started.");
            await _coinService.SetCoins();
            _logger.LogInformation("JobSetCoins finished.");
        }
    }
}
