using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.GameStats;
using Assets._Project.Develop.Runtime.Meta.Features.Progress;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.GameStats;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuPresentersFactory
    {
        private readonly DIContainer _container;

        public MainMenuPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public GameStatsPresenter CreateGameStatsPresenter(TextView winsView, TextView lossesView)
        {
            GameStatsService gameStatsService = _container.Resolve<GameStatsService>();

            return new GameStatsPresenter(
                gameStatsService.Wins,
                gameStatsService.Losses,
                winsView,
                lossesView);
        }

        public MainMenuScreenPresenter CreateMainMenuScreenPresenter(MainMenuScreenView view)
        {
            return new MainMenuScreenPresenter(
                view,
                _container.Resolve<ProjectPresentersFactory>(),
                this,
                _container.Resolve<ProgressResetService>());
        }
    }
}