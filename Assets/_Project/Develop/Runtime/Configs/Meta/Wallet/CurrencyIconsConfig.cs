using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.Wallet
{
    [CreateAssetMenu(fileName = "CurrencyIconsConfig", menuName = "Configs/Meta/Wallet/CurrencyIconsConfig")]
    public class CurrencyIconsConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyIconConfig> _configs;

        public Sprite GetSpriteFor(CurrencyTypes currencyType)
        {
            foreach (CurrencyIconConfig config in _configs)
            {
                if (config.Type == currencyType)
                    return config.Sprite;
            }

            throw new InvalidOperationException($"В {nameof(CurrencyIconsConfig)} нет иконки {currencyType}");
        }

        [Serializable]
        private class CurrencyIconConfig
        {
            [field: SerializeField] public CurrencyTypes Type { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
        }
    }
}