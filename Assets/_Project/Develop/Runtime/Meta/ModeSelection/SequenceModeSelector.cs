using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Gameplay.Sequence;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.ModeSelection
{
    public class SequenceModeSelector
    {
        private const KeyCode NumbersKey = KeyCode.Alpha1;
        private const KeyCode LettersKey = KeyCode.Alpha2;

        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        private bool _isSelected;

        public SequenceModeSelector(SceneSwitcherService sceneSwitcherService, ICoroutinesPerformer coroutinesPerformer)
        {
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
        }

        public void Update()
        {
            if (_isSelected)
                return;

            if (Input.GetKeyDown(NumbersKey))
                Select(SequenceMode.Numbers);
            else if (Input.GetKeyDown(LettersKey))
                Select(SequenceMode.Letters);
        }

        private void Select(SequenceMode mode)
        {
            _isSelected = true;

            Debug.Log($"Выбран режим: {mode}");

            _coroutinesPerformer.StartPerform(
                _sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(mode)));
        }
    }
}