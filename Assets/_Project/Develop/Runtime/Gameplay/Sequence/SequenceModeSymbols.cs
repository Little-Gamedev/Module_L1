using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Sequence
{
    [Serializable]
    public class SequenceModeSymbols
    {
        [field: SerializeField] public SequenceMode Mode { get; private set; }
        [field: SerializeField] public string Symbols { get; private set; }
    }
}