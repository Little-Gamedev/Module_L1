using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Gameplay.Results;
using Assets._Project.Develop.Runtime.Gameplay.Sequence;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Cycle
{
    public class GameplayCycle : IDisposable
    {
        private const KeyCode ContinueKey = KeyCode.Space;

        private readonly SequenceGenerator _sequenceGenerator;
        private readonly SequenceInputChecker _inputChecker;
        private readonly GameResultService _gameResultService;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly GameplayInputArgs _inputArgs;

        private bool _isFinished;
        private bool _isWin;
        private bool _isSwitching;

        public GameplayCycle(
            SequenceGenerator sequenceGenerator,
            SequenceInputChecker inputChecker,
            GameResultService gameResultService,
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer,
            GameplayInputArgs inputArgs)
        {
            _sequenceGenerator = sequenceGenerator;
            _inputChecker = inputChecker;
            _gameResultService = gameResultService;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _inputArgs = inputArgs;
        }

        public void Start()
        {
            _inputChecker.Succeeded += OnSucceeded;
            _inputChecker.Failed += OnFailed;

            string sequence = _sequenceGenerator.Generate();

            Debug.Log($"Введите последовательность: {sequence}");

            _inputChecker.Start(sequence);
        }

        public void Update()
        {
            if (_isFinished == false)
            {
                _inputChecker.Update();
                return;
            }

            if (_isSwitching || Input.GetKeyDown(ContinueKey) == false)
                return;

            _isSwitching = true;

            if (_isWin)
                SwitchTo(Scenes.MainMenu);
            else
                SwitchTo(Scenes.Gameplay, _inputArgs);
        }

        public void Dispose()
        {
            _inputChecker.Succeeded -= OnSucceeded;
            _inputChecker.Failed -= OnFailed;
            //ТЕСТ
            Debug.Log("GameplayCycle освобождён контейнером");
            //КОНЕЦ ТЕСТА
        }

        private void OnSucceeded()
        {
            Finish(true);
            _gameResultService.ApplyWin();
            Debug.Log($"Победа! {ContinueKey} — в главное меню");
        }

        private void OnFailed()
        {
            Finish(false);
            _gameResultService.ApplyLoss();
            Debug.Log($"Поражение! {ContinueKey} — начать заново");
        }

        private void Finish(bool isWin)
        {
            _isFinished = true;
            _isWin = isWin;
        }

        private void SwitchTo(string sceneName, IInputSceneArgs sceneArgs = null)
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(sceneName, sceneArgs));
        }
    }
}