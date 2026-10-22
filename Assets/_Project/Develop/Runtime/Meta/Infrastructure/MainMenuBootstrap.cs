using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.ModeSelection;
using Assets._Project.Develop.Runtime.Meta.ProgressMenu;
using Assets._Project.Develop.Runtime.UI;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Wallet;
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

        //ТЕСТ
        private CurrencyPresenter _testCurrencyPresenter;
        //КОНЕЦ ТЕСТА

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            _modeSelector = _container.Resolve<SequenceModeSelector>();
            _progressMenuInput = _container.Resolve<ProgressMenuInput>();

            //ТЕСТ
            UIRoot testUIRoot = _container.Resolve<UIRoot>();
            IconTextView testCurrencyView = _container.Resolve<ViewsFactory>()
                .Create<IconTextView>(ViewIDs.CurrencyView, testUIRoot.HUDLayer);
            _testCurrencyPresenter = _container.Resolve<ProjectPresentersFactory>()
                .CreateCurrencyPresenter(testCurrencyView, CurrencyTypes.Gold);
            _testCurrencyPresenter.Initialize();
            //КОНЕЦ ТЕСТА

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

            //ТЕСТ
            if (Input.GetKeyDown(KeyCode.G))
                _container.Resolve<WalletService>().Add(CurrencyTypes.Gold, 10);
            //КОНЕЦ ТЕСТА
        }

        //ТЕСТ
        private void OnDestroy()
        {
            _testCurrencyPresenter?.Dispose();
        }
        //КОНЕЦ ТЕСТА
    }
}