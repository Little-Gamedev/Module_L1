using System;
using Random = UnityEngine.Random;

namespace Assets._Project.Develop.Runtime.Gameplay.Sequence
{
    public class SequenceGenerator
    {
        private readonly string _symbols;
        private readonly int _length;

        public SequenceGenerator(string symbols, int length)
        {
            if (string.IsNullOrEmpty(symbols))
                throw new ArgumentException("Не может быть пустым", nameof(symbols));

            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Длина должна быть больше нуля");

            _symbols = symbols;
            _length = length;
        }

        public string Generate()
        {
            char[] sequence = new char[_length];

            for (int i = 0; i < _length; i++)
                sequence[i] = _symbols[Random.Range(0, _symbols.Length)];

            return new string(sequence);
        }
    }
}