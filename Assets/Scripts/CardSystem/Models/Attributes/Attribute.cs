using System;

namespace Assets.Scripts.CardSystem.Model
{
    public class Attribute
    {
        private int _value;

        public Attribute(string name, int value = 0)
        {
            Name = name;
            Value = value;
        }

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke(this);
            }
        }

        public string Name { get; set; }


        public Action<Attribute> OnValueChanged { get; set; }
    }
}