using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.ModeSelection;
using Assets._Project.Develop.Runtime.Meta.ProgressMenu;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private SequenceModeSelector _modeSelector;
        private ProgressMenuInput _progressMenuInput;

        private bool _isRunning;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация сцены меню");

            _modeSelector = _container.Resolve<SequenceModeSelector>();
            _progressMenuInput = _container.Resolve<ProgressMenuInput>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Главное меню. 1 — цифры, 2 — буквы, I — прогресс, R — сбросить прогресс");

            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;

            _modeSelector.Update();
            _progressMenuInput.Update();
        }
    }
}