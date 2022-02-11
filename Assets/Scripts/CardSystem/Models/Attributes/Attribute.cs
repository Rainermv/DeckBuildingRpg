using System;
using Assets.Scripts.Ruleset;

namespace Assets.Scripts.CardSystem.Models.Attributes
{
    public class Attribute
    {
        private readonly AttributeKey _key;
        private int _value;

        private Action<AttributeKey, int> OnValueChanged { get; set; }

        public Attribute(AttributeKey key, int value, Action<AttributeKey, int> onValueChanged)
        {
            _key = key;
            Value = value;
            OnValueChanged = onValueChanged;
        }

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke(_key, value);
            }
        }

        
    }
}