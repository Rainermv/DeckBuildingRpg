using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.CardSystem.Model.Collection;

namespace Assets.Scripts.CardSystem.Model
{
    public class AttributeSet
    {
        private Dictionary<string, Attribute> _attributeDictionary = new();
        public Action<string, int> OnAttributeValueChange { get; set; }

        /// <summary>
        /// Gets the value of an attribute using attributeName as key
        /// Creates a new attribute if the attribute does not exist and return its value
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public int GetValue(string attributeName)
        {
            if (_attributeDictionary.TryGetValue(attributeName, out var attribute))
            {
                return attribute.Value;
            };

            return Add(attributeName).Value;
        }

       
        public Attribute Set(string attributeName, int value)
        {
            if (_attributeDictionary.TryGetValue(attributeName, out var attribute))
            {
                attribute.Value = value;
                return attribute;
            };

            return Add(attributeName, value);
        }

        public Attribute Sum(string attributeName, int sumValue)
        {
            if (_attributeDictionary.TryGetValue(attributeName, out var attribute))
            {
                attribute.Value += sumValue;
                return attribute;
            };

            return Add(attributeName, sumValue);
        }

        private Attribute Add(string attributeName, int value = 0)
        {
            var attribute = new Attribute(attributeName, value, 
                (s, i) => OnAttributeValueChange?.Invoke(s,i));
            _attributeDictionary.Add(attributeName, attribute);
            return attribute;
        }


        
    }
}