using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Sequence
{
    public class SequenceInputChecker
    {
        public event Action Succeeded;
        public event Action Failed;

        private string _sequence;
        private int _currentIndex;
        private bool _isChecking;

        public void Start(string sequence)
        {
            if (string.IsNullOrEmpty(sequence))
                throw new ArgumentException("Не может быть пустой", nameof(sequence));

            _sequence = sequence;
            _currentIndex = 0;
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
            char expectedSymbol = _sequence[_currentIndex];

            if (char.ToUpperInvariant(inputSymbol) != char.ToUpperInvariant(expectedSymbol))
            {
                _isChecking = false;
                Failed?.Invoke();

                return;
            }

            Debug.Log($"Верно: {expectedSymbol}");

            _currentIndex++;

            if (_currentIndex < _sequence.Length)
                return;

            _isChecking = false;
            Succeeded?.Invoke();
        }
    }
}