using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Events;

namespace Assets.Scripts.Core.Model.AttributeModel
{
    public class Attributes
    {
        private Dictionary<int, Attribute> _attributeDictionary = new();
        public Action<int, int> OnAttributeValueChange { get; set; }

        public int GetValue(int attributeKey)
        {
            if (_attributeDictionary.TryGetValue(attributeKey, out var attribute))
            {
                return attribute.Value;
                
            };

            DebugEvents.Log(this, $"Failed to Get value from {attributeKey}. Attribute not on Dictionary (returning 0)");

            return 0;
        }


        public void SetValue(int attributeKey, int value)
        {
            if (_attributeDictionary.TryGetValue(attributeKey, out var attribute))
            {
                attribute.Value = value;
                return;
            };

            DebugEvents.Log(this, $"Failed to Set value {value} on {attributeKey}. Attribute not on Dictionary");

        }

        public void ModifyValue(int attributeKey, int sumValue)
        {
            if (_attributeDictionary.TryGetValue(attributeKey, out var attribute))
            {
                attribute.Value += sumValue;
                return;
            };

            DebugEvents.Log(this, $"Failed to Modify value {sumValue} on {attributeKey}. Attribute not on Dictionary");

        }

        public void Add(int attributeKey, int value = 0)
        {
            var attribute = new Attribute(attributeKey, value, 
                (key, value) => OnAttributeValueChange?.Invoke(key,value));

            _attributeDictionary.Add(attributeKey, attribute);
            DebugEvents.Log(this, $"Added {attributeKey} to Dictionary");

        }


        public bool Contains(int attributeKey)
        {
            return _attributeDictionary.ContainsKey(attributeKey);
        }
    }
}