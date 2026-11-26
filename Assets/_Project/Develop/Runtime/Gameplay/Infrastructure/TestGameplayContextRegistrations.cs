
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class TestGameplayContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateResourcesAssetsLoader);
            container.RegisterAsSingle(CreateEntitiesLifeContext);
            container.RegisterAsSingle(CreateEntitiesFactory);
            container.RegisterAsSingle(CreateMonoEntitiesFactory).NonLazy();
        }

        private static ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c)
            => new ResourcesAssetsLoader();

        private static EntitiesLifeContext CreateEntitiesLifeContext(DIContainer c)
            => new EntitiesLifeContext();

        private static EntitiesFactory CreateEntitiesFactory(DIContainer c)
            => new EntitiesFactory(c);

        private static MonoEntitiesFactory CreateMonoEntitiesFactory(DIContainer c)
            => new MonoEntitiesFactory(
                c.Resolve<ResourcesAssetsLoader>(),
                c.Resolve<EntitiesLifeContext>());
    }
}
