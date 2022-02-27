using System;
using System.Collections.Generic;

namespace Assets.Scripts.Core.Model.AttributeModel
{
    public class AttributeSet
    {
        private Dictionary<int, Attribute> _attributeDictionary = new();
        public Action<int, int> OnAttributeValueChange { get; set; }

        /// <summary>
        /// Gets the value of an attribute using attributeName as key
        /// Creates a new attribute if the attribute does not exist and return its value
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public int GetValue(int attributeKey)
        {
            if (_attributeDictionary.TryGetValue(attributeKey, out var attribute))
            {
                return attribute.Value;
                
            };

            MDebug.Log(this, $"Failed to Get value from {attributeKey}. Attribute not on Dictionary (returning 0)");

            return 0;
        }

       
        public void Set(int attributeKey, int value)
        {
            if (_attributeDictionary.TryGetValue(attributeKey, out var attribute))
            {
                attribute.Value = value;
                return;
            };

            MDebug.Log(this, $"Failed to Set value {value} on {attributeKey}. Attribute not on Dictionary");

        }

        public void Modify(int attributeKey, int sumValue)
        {
            if (_attributeDictionary.TryGetValue(attributeKey, out var attribute))
            {
                attribute.Value += sumValue;
                return;
            };

            MDebug.Log(this, $"Failed to Modify value {sumValue} on {attributeKey}. Attribute not on Dictionary");

        }

        public void Add(int attributeKey, int value = 0)
        {
            var attribute = new Attribute(attributeKey, value, 
                (key, value) => OnAttributeValueChange?.Invoke(key,value));

            _attributeDictionary.Add(attributeKey, attribute);
            MDebug.Log(this, $"Added {attributeKey} to Dictionary");

        }


        public bool Contains(int attributeKey)
        {
            return _attributeDictionary.ContainsKey(attributeKey);
        }
    }
}