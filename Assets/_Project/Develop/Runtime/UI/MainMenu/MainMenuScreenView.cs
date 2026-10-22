using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action ResetProgressClicked;

        [SerializeField] private Button _resetProgressButton;
        [SerializeField] private TextView _resetProgressButtonText;

        [field: SerializeField] public IconTextListView WalletView { get; private set; }
        [field: SerializeField] public TextView WinsView { get; private set; }
        [field: SerializeField] public TextView LossesView { get; private set; }
        [field: SerializeField] public TextView MessageView { get; private set; }

        private void OnEnable()
        {
            _resetProgressButton.onClick.AddListener(OnResetProgressButtonClicked);
        }

        private void OnDisable()
        {
            _resetProgressButton.onClick.RemoveListener(OnResetProgressButtonClicked);
        }

        public void SetResetProgressButtonText(string text) => _resetProgressButtonText.SetText(text);

        private void OnResetProgressButtonClicked() => ResetProgressClicked?.Invoke();
    }
}