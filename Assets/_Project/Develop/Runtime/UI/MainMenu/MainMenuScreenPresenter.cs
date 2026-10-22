using Assets._Project.Develop.Runtime.Meta.Features.Progress;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.GameStats;
using Assets._Project.Develop.Runtime.UI.Wallet;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _view;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly MainMenuPresentersFactory _mainMenuPresentersFactory;
        private readonly ProgressResetService _progressResetService;

        private WalletPresenter _walletPresenter;
        private GameStatsPresenter _gameStatsPresenter;

        public MainMenuScreenPresenter(
            MainMenuScreenView view,
            ProjectPresentersFactory projectPresentersFactory,
            MainMenuPresentersFactory mainMenuPresentersFactory,
            ProgressResetService progressResetService)
        {
            _view = view;
            _projectPresentersFactory = projectPresentersFactory;
            _mainMenuPresentersFactory = mainMenuPresentersFactory;
            _progressResetService = progressResetService;
        }

        public void Initialize()
        {
            _walletPresenter = _projectPresentersFactory.CreateWalletPresenter(_view.WalletView);
            _walletPresenter.Initialize();

            _gameStatsPresenter = _mainMenuPresentersFactory.CreateGameStatsPresenter(_view.WinsView, _view.LossesView);
            _gameStatsPresenter.Initialize();

            _view.SetResetProgressButtonText($"Сбросить прогресс ({_progressResetService.Price})");
            _view.MessageView.SetText(string.Empty);

            _view.ResetProgressClicked += OnResetProgressClicked;
        }

        public void Dispose()
        {
            _view.ResetProgressClicked -= OnResetProgressClicked;

            _gameStatsPresenter?.Dispose();
            _walletPresenter?.Dispose();
        }

        private void OnResetProgressClicked()
        {
            if (_progressResetService.TryReset())
                _view.MessageView.SetText($"Прогресс сброшен за {_progressResetService.Price} моенит");
            else
                _view.MessageView.SetText($"Недостаточно монет: нужно {_progressResetService.Price}");
        }
    }
}