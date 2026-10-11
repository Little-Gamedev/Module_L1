using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.ModeSelection;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private SequenceModeSelector _modeSelector;

        private bool _isRunning;

        //ТЕСТ
        private PlayerData _testData;
        private ISaveLoadService _testSaveLoadService;
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
            _testSaveLoadService = _container.Resolve<ISaveLoadService>();
            _testCoroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            //КОНЕЦ ТЕСТА

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Главное меню. Нажмите 1 — цифры, 2 — буквы");

            //ТЕСТ
            _testData = new PlayerData()
            {
                WalletData = new Dictionary<CurrencyTypes, int>()
                {
                    { CurrencyTypes.Gold, 150 },
                },
                Wins = 3,
                Losses = 1,
            };
            //КОНЕЦ ТЕСТА

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
                _testCoroutinesPerformer.StartPerform(_testSaveLoadService.Save(_testData));
                Debug.Log("Сохранение вызвано");
            }

            if (Input.GetKeyDown(KeyCode.D))
                _testCoroutinesPerformer.StartPerform(TestLoad());
            //КОНЕЦ ТЕСТА
        }

        //ТЕСТ
        private IEnumerator TestLoad()
        {
            PlayerData loaded = null;

            yield return _testSaveLoadService.Load<PlayerData>(data => loaded = data);

            Debug.Log($"Из файла — золото: {loaded.WalletData[CurrencyTypes.Gold]}, победы: {loaded.Wins}, поражения: {loaded.Losses}");
        }
        //КОНЕЦ ТЕСТА
    }
}