using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.UI.GameStats
{
    public class GameStatsPresenter : IPresenter
    {
        private readonly IReadOnlyVariable<int> _wins;
        private readonly IReadOnlyVariable<int> _losses;

        private readonly TextView _winsView;
        private readonly TextView _lossesView;

        private IDisposable _winsSubscription;
        private IDisposable _lossesSubscription;

        public GameStatsPresenter(
            IReadOnlyVariable<int> wins,
            IReadOnlyVariable<int> losses,
            TextView winsView,
            TextView lossesView)
        {
            _wins = wins;
            _losses = losses;
            _winsView = winsView;
            _lossesView = lossesView;
        }

        public void Initialize()
        {
            UpdateWins(_wins.Value);
            UpdateLosses(_losses.Value);

            _winsSubscription = _wins.Subscribe(OnWinsChanged);
            _lossesSubscription = _losses.Subscribe(OnLossesChanged);
        }

        public void Dispose()
        {
            _winsSubscription?.Dispose();
            _lossesSubscription?.Dispose();
        }

        private void OnWinsChanged(int oldValue, int newValue) => UpdateWins(newValue);

        private void OnLossesChanged(int oldValue, int newValue) => UpdateLosses(newValue);

        private void UpdateWins(int value) => _winsView.SetText($"Победы: {value}");

        private void UpdateLosses(int value) => _lossesView.SetText($"Поражения: {value}");
    }
}