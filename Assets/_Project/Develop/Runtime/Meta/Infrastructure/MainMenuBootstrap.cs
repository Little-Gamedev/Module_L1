using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.ModeSelection;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private SequenceModeSelector _modeSelector;

        private bool _isRunning;

        //ТЕСТ
        private PlayerDataProvider _testPlayerDataProvider;
        private ICoroutinesPerformer _testCoroutinesPerformer;
        //КОНЕЦ ТЕСТА

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация сцены меню");

            _modeSelector = _container.Resolve<SequenceModeSelector>();

            //ТЕСТ
            _testPlayerDataProvider = _container.Resolve<PlayerDataProvider>();
            _testCoroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            //КОНЕЦ ТЕСТА

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Главное меню. Нажмите 1 — цифры, 2 — буквы");

            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;

            _modeSelector.Update();

            //ТЕСТ
            if (Input.GetKeyDown(KeyCode.S))
            {
                _testCoroutinesPerformer.StartPerform(_testPlayerDataProvider.Save());
                Debug.Log("Сохранение через провайдер вызвано");
            }
            //КОНЕЦ ТЕСТА
        }
    }
}