using System.Collections.Generic;
using Assets.Scripts.CardSystem.Model.Collection;

namespace Assets.Scripts.CardSystem.Model
{
    public class AttributeSet
    {
        private Dictionary<string, Attribute> _attributeDictionary = new();

        /// <summary>
        /// Gets the value of an attribute using resourceName as key
        /// Creates a new resource if the resource does not exist and return its value
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public int GetValue(string resourceName)
        {
            if (_attributeDictionary.TryGetValue(resourceName, out var resource))
            {
                return resource.Value;
            };

            return Add(resourceName).Value;
        }

        /// <summary>
        /// Gets an attribute using resourceName as key
        /// Creates a new resource if the resource does not exist
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public Attribute Get(string resourceName)
        {
            if (_attributeDictionary.TryGetValue(resourceName, out var resource))
            {
                return resource;
            };

            return Add(resourceName);
        }

        public Attribute Set(string resourceName, int value)
        {
            if (_attributeDictionary.TryGetValue(resourceName, out var resource))
            {
                resource.Value = value;
                return resource;
            };

            return Add(resourceName, value);
        }

        private Attribute Add(string resourceName, int value = 0)
        {
            var resource = new Attribute(resourceName, value);
            _attributeDictionary.Add(resourceName, resource);
            return resource;
        }

        
    }
}