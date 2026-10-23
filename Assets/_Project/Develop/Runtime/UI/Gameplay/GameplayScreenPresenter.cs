using Assets._Project.Develop.Runtime.Gameplay.Cycle;
using Assets._Project.Develop.Runtime.Gameplay.Sequence;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Wallet;
using System;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _view;
        private readonly SequenceInputChecker _inputChecker;
        private readonly GameplayCycle _gameplayCycle;
        private readonly ProjectPresentersFactory _projectPresentersFactory;

        private WalletPresenter _walletPresenter;

        private IDisposable _sequenceSubscription;
        private IDisposable _typedSequenceSubscription;

        public GameplayScreenPresenter(
            GameplayScreenView view,
            SequenceInputChecker inputChecker,
            GameplayCycle gameplayCycle,
            ProjectPresentersFactory projectPresentersFactory)
        {
            _view = view;
            _inputChecker = inputChecker;
            _gameplayCycle = gameplayCycle;
            _projectPresentersFactory = projectPresentersFactory;
        }

        public void Initialize()
        {
            _walletPresenter = _projectPresentersFactory.CreateWalletPresenter(_view.WalletView);
            _walletPresenter.Initialize();

            UpdateSequence(_inputChecker.Sequence.Value);
            UpdateTypedSequence(_inputChecker.TypedSequence.Value);
            _view.ResultView.SetText(string.Empty);

            _sequenceSubscription = _inputChecker.Sequence.Subscribe(OnSequenceChanged);
            _typedSequenceSubscription = _inputChecker.TypedSequence.Subscribe(OnTypedSequenceChanged);

            _gameplayCycle.Won += OnWon;
            _gameplayCycle.Lost += OnLost;
        }

        public void Dispose()
        {
            _gameplayCycle.Won -= OnWon;
            _gameplayCycle.Lost -= OnLost;

            _sequenceSubscription?.Dispose();
            _typedSequenceSubscription?.Dispose();

            _walletPresenter?.Dispose();
        }

        private void OnSequenceChanged(string oldValue, string newValue) => UpdateSequence(newValue);

        private void OnTypedSequenceChanged(string oldValue, string newValue) => UpdateTypedSequence(newValue);

        private void UpdateSequence(string sequence) => _view.SequenceView.SetText($"Загадано: {sequence}");

        private void UpdateTypedSequence(string typedSequence) => _view.TypedSequenceView.SetText($"Ваш ввод: {typedSequence}");

        private void OnWon() => _view.ResultView.SetText($"Победа! {GameplayCycle.ContinueKey} — в главное меню");

        private void OnLost() => _view.ResultView.SetText($"Поражение! {GameplayCycle.ContinueKey} — начать заново");
    }
}