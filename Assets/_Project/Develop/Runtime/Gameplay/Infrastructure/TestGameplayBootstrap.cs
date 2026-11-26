
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class TestGameplayBootstrap : MonoBehaviour
    {
        [SerializeField] private TestGameplay _testGameplay;

        private DIContainer _container;
        private EntitiesLifeContext _entitiesLifeContext;

        private bool _isRunning;

        private void Start()
        {
            _container = new DIContainer();

            TestGameplayContextRegistrations.Process(_container);

            _container.Initialize();

            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();

            _testGameplay.Initialize(_container);
            _testGameplay.Run();

            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;

            _testGameplay.UpdateInput();

            _entitiesLifeContext.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _isRunning = false;

            _container?.Dispose();
        }
    }
}
