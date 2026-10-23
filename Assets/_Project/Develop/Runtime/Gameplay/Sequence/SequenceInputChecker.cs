using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Sequence
{
    public class SequenceInputChecker
    {
        public event Action Succeeded;
        public event Action Failed;

        private readonly ReactiveVariable<string> _sequence = new(string.Empty);
        private readonly ReactiveVariable<string> _typedSequence = new(string.Empty);

        private bool _isChecking;

        public IReadOnlyVariable<string> Sequence => _sequence;
        public IReadOnlyVariable<string> TypedSequence => _typedSequence;

        public void Start(string sequence)
        {
            if (string.IsNullOrEmpty(sequence))
                throw new ArgumentException("Не может быть пустой", nameof(sequence));

            _sequence.Value = sequence;
            _typedSequence.Value = string.Empty;
            _isChecking = true;
        }

        public void Update()
        {
            if (_isChecking == false)
                return;

            foreach (char inputSymbol in Input.inputString)
            {
                if (char.IsWhiteSpace(inputSymbol) || char.IsControl(inputSymbol))
                    continue;

                CheckSymbol(inputSymbol);

                if (_isChecking == false)
                    return;
            }
        }

        private void CheckSymbol(char inputSymbol)
        {
            int currentIndex = _typedSequence.Value.Length;
            char expectedSymbol = _sequence.Value[currentIndex];
            char typedSymbol = char.ToUpperInvariant(inputSymbol);

            _typedSequence.Value += typedSymbol;

            if (typedSymbol != char.ToUpperInvariant(expectedSymbol))
            {
                _isChecking = false;
                Failed?.Invoke();

                return;
            }

            if (_typedSequence.Value.Length < _sequence.Value.Length)
                return;

            _isChecking = false;
            Succeeded?.Invoke();
        }
    }
}