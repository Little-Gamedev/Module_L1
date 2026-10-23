using Assets._Project.Develop.Runtime.Gameplay.Cycle;
using Assets._Project.Develop.Runtime.Gameplay.Sequence;
using Assets._Project.Develop.Runtime.Infrastructure.DI;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayPresentersFactory
    {
        private readonly DIContainer _container;

        public GameplayPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public GameplayScreenPresenter CreateGameplayScreenPresenter(GameplayScreenView view)
        {
            return new GameplayScreenPresenter(
                view,
                _container.Resolve<SequenceInputChecker>(),
                _container.Resolve<GameplayCycle>(),
                _container.Resolve<ProjectPresentersFactory>());
        }
    }
}