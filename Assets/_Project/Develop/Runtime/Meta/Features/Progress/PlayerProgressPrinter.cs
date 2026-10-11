using Assets._Project.Develop.Runtime.Meta.Features.GameStats;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features.Progress
{
    public class PlayerProgressPrinter
    {
        private readonly WalletService _walletService;
        private readonly GameStatsService _gameStatsService;

        public PlayerProgressPrinter(WalletService walletService, GameStatsService gameStatsService)
        {
            _walletService = walletService;
            _gameStatsService = gameStatsService;
        }

        public void Print()
        {
            Debug.Log($"Победы: {_gameStatsService.Wins.Value}, " +
                $"поражения: {_gameStatsService.Losses.Value}, " +
                $"золото: {_walletService.GetCurrency(CurrencyTypes.Gold).Value}");
        }
    }
}