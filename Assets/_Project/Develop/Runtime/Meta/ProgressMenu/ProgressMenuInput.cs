using Assets._Project.Develop.Runtime.Meta.Features.Progress;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.ProgressMenu
{
    public class ProgressMenuInput
    {
        private const KeyCode PrintKey = KeyCode.I;
        private const KeyCode ResetKey = KeyCode.R;

        private readonly PlayerProgressPrinter _progressPrinter;
        private readonly ProgressResetService _progressResetService;

        public ProgressMenuInput(PlayerProgressPrinter progressPrinter, ProgressResetService progressResetService)
        {
            _progressPrinter = progressPrinter;
            _progressResetService = progressResetService;
        }

        public void Update()
        {
            if (Input.GetKeyDown(PrintKey))
                _progressPrinter.Print();

            if (Input.GetKeyDown(ResetKey))
                _progressResetService.TryReset();
        }
    }
}