using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Wallet
{
    public class CurrencyPresenter : IPresenter
    {
        private readonly IReadOnlyVariable<int> _currency;
        private readonly Sprite _icon;

        private readonly IconTextView _view;

        private IDisposable _currencySubscription;

        public CurrencyPresenter(IReadOnlyVariable<int> currency, Sprite icon, IconTextView view)
        {
            _currency = currency;
            _icon = icon;
            _view = view;
        }

        public IconTextView View => _view;

        public void Initialize()
        {
            _view.SetIcon(_icon);
            UpdateValue(_currency.Value);

            _currencySubscription = _currency.Subscribe(OnCurrencyChanged);
        }

        public void Dispose()
        {
            _currencySubscription?.Dispose();
        }

        private void OnCurrencyChanged(int oldValue, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}