using System;

namespace Assets.Scripts.Model.AttributeModel
{
    public class Attribute
    {
        private readonly int _key;
        private int _value;

        private Action<int, int> OnValueChanged { get; set; }

        public Attribute(int key, int value, Action<int, int> onValueChanged)
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