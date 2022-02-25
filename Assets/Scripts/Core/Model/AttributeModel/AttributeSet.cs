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

            return Add(attributeKey).Value;
        }

       
        public Attribute Set(int attributeKey, int value)
        {
            if (_attributeDictionary.TryGetValue(attributeKey, out var attribute))
            {
                attribute.Value = value;
                return attribute;
            };

            return Add(attributeKey, value);
        }

        public Attribute Modify(int attributeKey, int sumValue)
        {
            if (_attributeDictionary.TryGetValue(attributeKey, out var attribute))
            {
                attribute.Value += sumValue;
                return attribute;
            };

            return Add(attributeKey, sumValue);
        }

        private Attribute Add(int attributeKey, int value = 0)
        {
            var attribute = new Attribute(attributeKey, value, 
                (key, value) => OnAttributeValueChange?.Invoke(key,value));
            _attributeDictionary.Add(attributeKey, attribute);
            return attribute;
        }


        public bool Contains(int attributeKey)
        {
            return _attributeDictionary.ContainsKey(attributeKey);
        }
    }
}