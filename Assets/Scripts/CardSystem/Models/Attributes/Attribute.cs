using System;

namespace Assets.Scripts.CardSystem.Model
{
    public class Attribute
    {
        private readonly string _name;
        private int _value;

        private Action<string, int> OnValueChanged { get; set; }

        public Attribute(string name, int value, Action<string, int> onValueChanged)
        {
            _name = name;
            Value = value;
            OnValueChanged = onValueChanged;
        }

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke(_name, value);
            }
        }

        
    }
}