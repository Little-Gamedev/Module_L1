using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Gameplay.Cycle;
using Assets._Project.Develop.Runtime.Gameplay.Results;
using Assets._Project.Develop.Runtime.Gameplay.Sequence;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.GameStats;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            container.RegisterAsSingle(c => CreateSequenceGenerator(c, args));
            container.RegisterAsSingle(CreateSequenceInputChecker);
            container.RegisterAsSingle(CreateGameResultService);
            container.RegisterAsSingle(c => CreateGameplayCycle(c, args));
        }

        private static SequenceGenerator CreateSequenceGenerator(DIContainer c, GameplayInputArgs args)
        {
            SequenceConfig config = c.Resolve<ConfigsProviderService>().GetConfig<SequenceConfig>();

            return new SequenceGenerator(config.GetSymbols(args.Mode), config.SequenceLength);
        }

        private static SequenceInputChecker CreateSequenceInputChecker(DIContainer c)
            => new SequenceInputChecker();

        private static GameResultService CreateGameResultService(DIContainer c)
            => new GameResultService(
                c.Resolve<WalletService>(),
                c.Resolve<GameStatsService>(),
                c.Resolve<PlayerDataProvider>(),
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<ConfigsProviderService>().GetConfig<GameRewardsConfig>());

        private static GameplayCycle CreateGameplayCycle(DIContainer c, GameplayInputArgs args)
            => new GameplayCycle(
                c.Resolve<SequenceGenerator>(),
                c.Resolve<SequenceInputChecker>(),
                c.Resolve<GameResultService>(),
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<ICoroutinesPerformer>(),
                args);
    }
}