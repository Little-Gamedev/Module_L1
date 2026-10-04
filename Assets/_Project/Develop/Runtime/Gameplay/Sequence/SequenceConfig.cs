using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Sequence
{
    [CreateAssetMenu(fileName = "SequenceConfig", menuName = "Configs/Sequence")]
    public class SequenceConfig : ScriptableObject
    {
        [SerializeField] private List<SequenceModeSymbols> _modesSymbols;

        [field: SerializeField, Min(1)] public int SequenceLength { get; private set; } = 5;

        public string GetSymbols(SequenceMode mode)
        {
            foreach (SequenceModeSymbols modeSymbols in _modesSymbols)
            {
                if (modeSymbols.Mode == mode)
                    return modeSymbols.Symbols;
            }

            throw new InvalidOperationException($"В {nameof(SequenceConfig)} нет символов для режима {mode}");
        }
    }
}