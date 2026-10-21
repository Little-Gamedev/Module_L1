using Assets._Project.Develop.Runtime.Gameplay.Cycle;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System;
using System.Collections;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private GameplayCycle _gameplayCycle;

        private bool _isRunning;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} не подходит к типу {typeof(GameplayInputArgs)}");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            _gameplayCycle = _container.Resolve<GameplayCycle>();

            yield break;
        }

        public override void Run()
        {
            _gameplayCycle.Start();

            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;

            _gameplayCycle.Update();
        }
    }
}