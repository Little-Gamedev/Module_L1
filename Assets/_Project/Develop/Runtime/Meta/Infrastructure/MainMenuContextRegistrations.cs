using Assets._Project.Develop.Runtime.Configs.Meta.Progress;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.GameStats;
using Assets._Project.Develop.Runtime.Meta.Features.Progress;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.ModeSelection;
using Assets._Project.Develop.Runtime.Meta.ProgressMenu;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateSequenceModeSelector);
            container.RegisterAsSingle(CreatePlayerProgressPrinter);
            container.RegisterAsSingle(CreateProgressResetService);
            container.RegisterAsSingle(CreateProgressMenuInput);
        }

        private static SequenceModeSelector CreateSequenceModeSelector(DIContainer c)
            => new SequenceModeSelector(
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<ICoroutinesPerformer>());

        private static PlayerProgressPrinter CreatePlayerProgressPrinter(DIContainer c)
            => new PlayerProgressPrinter(
                c.Resolve<WalletService>(),
                c.Resolve<GameStatsService>());

        private static ProgressResetService CreateProgressResetService(DIContainer c)
            => new ProgressResetService(
                c.Resolve<WalletService>(),
                c.Resolve<GameStatsService>(),
                c.Resolve<PlayerDataProvider>(),
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<ConfigsProviderService>().GetConfig<ProgressResetConfig>());

        private static ProgressMenuInput CreateProgressMenuInput(DIContainer c)
            => new ProgressMenuInput(
                c.Resolve<PlayerProgressPrinter>(),
                c.Resolve<ProgressResetService>());
    }
}