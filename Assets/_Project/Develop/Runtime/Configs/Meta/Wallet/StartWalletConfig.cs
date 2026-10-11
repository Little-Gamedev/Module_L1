using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.Wallet
{
    [CreateAssetMenu(fileName = "StartWalletConfig", menuName = "Configs/Meta/Wallet/StartWalletConfig")]
    public class StartWalletConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _values;

        public int GetValueFor(CurrencyTypes currencyType)
        {
            foreach (CurrencyConfig config in _values)
            {
                if (config.Type == currencyType)
                    return config.Value;
            }

            throw new InvalidOperationException($"В {nameof(StartWalletConfig)} нет значения для валюты {currencyType}");
        }

        [Serializable]
        private class CurrencyConfig
        {
            [field: SerializeField] public CurrencyTypes Type { get; private set; }
            [field: SerializeField, Min(0)] public int Value { get; private set; }
        }
    }
}