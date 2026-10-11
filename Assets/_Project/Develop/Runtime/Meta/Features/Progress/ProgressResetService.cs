using Assets._Project.Develop.Runtime.Configs.Meta.Progress;
using Assets._Project.Develop.Runtime.Meta.Features.GameStats;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using UnityEngine;

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

        public void TryReset()
        {
            int price = _config.ResetPrice;

            if (_walletService.Enough(CurrencyTypes.Gold, price) == false)
            {
                int currentGold = _walletService.GetCurrency(CurrencyTypes.Gold).Value;
                Debug.Log($"Недостаточно золота для сброса побд и поражений: нужно {price}, есть {currentGold}");
                return;
            }

            _walletService.Spend(CurrencyTypes.Gold, price);
            _gameStatsService.Clear();

            _coroutinesPerformer.StartPerform(_playerDataProvider.Save());

            Debug.Log($"Прогресс сброшен за {price} монет");
        }
    }
}