using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Meta.Features.GameStats;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Results
{
    public class GameResultService
    {
        private readonly WalletService _walletService;
        private readonly GameStatsService _gameStatsService;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly GameRewardsConfig _config;

        public GameResultService(
            WalletService walletService,
            GameStatsService gameStatsService,
            PlayerDataProvider playerDataProvider,
            ICoroutinesPerformer coroutinesPerformer,
            GameRewardsConfig config)
        {
            _walletService = walletService;
            _gameStatsService = gameStatsService;
            _playerDataProvider = playerDataProvider;
            _coroutinesPerformer = coroutinesPerformer;
            _config = config;
        }

        public void ApplyWin()
        {
            _gameStatsService.AddWin();
            _walletService.Add(CurrencyTypes.Gold, _config.WinReward);

            Save();
        }

        public void ApplyLoss()
        {
            _gameStatsService.AddLoss();

            int currentGold = _walletService.GetCurrency(CurrencyTypes.Gold).Value;
            int penalty = Math.Min(_config.LossPenalty, currentGold);

            if (penalty > 0)
                _walletService.Spend(CurrencyTypes.Gold, penalty);

            Save();
        }

        private void Save()
            => _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
    }
}