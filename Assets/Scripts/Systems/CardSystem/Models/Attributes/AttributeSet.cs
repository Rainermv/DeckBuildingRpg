using System;
using System.Collections.Generic;
using Assets.Scripts.Ruleset;

namespace Assets.Scripts.CardSystem.Models.Attributes
{
    public class AttributeSet
    {
        private Dictionary<AttributeKey, Attribute> _attributeDictionary = new();
        public Action<AttributeKey, int> OnAttributeValueChange { get; set; }

        /// <summary>
        /// Gets the value of an attribute using attributeName as key
        /// Creates a new attribute if the attribute does not exist and return its value
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public int GetValue(AttributeKey attributeKey)
        {
            if (_attributeDictionary.TryGetValue(attributeKey, out var attribute))
            {
                return attribute.Value;
            };

            return Add(attributeKey).Value;
        }

       
        public Attribute Set(AttributeKey attributeKey, int value)
        {
            if (_attributeDictionary.TryGetValue(attributeKey, out var attribute))
            {
                attribute.Value = value;
                return attribute;
            };

            return Add(attributeKey, value);
        }

        public Attribute Sum(AttributeKey attributeKey, int sumValue)
        {
            if (_attributeDictionary.TryGetValue(attributeKey, out var attribute))
            {
                attribute.Value += sumValue;
                return attribute;
            };

            return Add(attributeKey, sumValue);
        }

        private Attribute Add(AttributeKey attributeKey, int value = 0)
        {
            var attribute = new Attribute(attributeKey, value, 
                (key, value) => OnAttributeValueChange?.Invoke(key,value));
            _attributeDictionary.Add(attributeKey, attribute);
            return attribute;
        }


        public bool Contains(AttributeKey attributeKey)
        {
            return _attributeDictionary.ContainsKey(attributeKey);
        }
    }
}