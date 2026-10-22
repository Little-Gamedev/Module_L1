using Assets._Project.Develop.Runtime.Configs.Meta.Progress;
using Assets._Project.Develop.Runtime.Meta.Features.GameStats;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;

namespace Assets._Project.Develop.Runtime.Meta.Features.Progress
{
    public class ProgressResetService
    {
        private readonly WalletService _walletService;
        private readonly GameStatsService _gameStatsService;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly ProgressResetConfig _config;

        public ProgressResetService(
            WalletService walletService,
            GameStatsService gameStatsService,
            PlayerDataProvider playerDataProvider,
            ICoroutinesPerformer coroutinesPerformer,
            ProgressResetConfig config)
        {
            _walletService = walletService;
            _gameStatsService = gameStatsService;
            _playerDataProvider = playerDataProvider;
            _coroutinesPerformer = coroutinesPerformer;
            _config = config;
        }

        public int Price => _config.ResetPrice;

        public bool TryReset()
        {
            if (_walletService.Enough(CurrencyTypes.Gold, Price) == false)
                return false;

            _walletService.Spend(CurrencyTypes.Gold, Price);
            _gameStatsService.Clear();

            _coroutinesPerformer.StartPerform(_playerDataProvider.Save());

            return true;
        }
    }
}