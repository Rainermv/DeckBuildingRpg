using System;

namespace Assets.Scripts.CardSystem.Model
{
    public class Resource
    {
        private int _value;

        public Resource(string name, int value = 0)
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


        public Action<Resource> OnValueChanged { get; set; }
    }
}