using TMPro;
using UnityEngine;

namespace Assets.Scripts.View.Attribute
{
    public class StringCardAttributeView : MonoBehaviour, ICardAttributeView
    {
        public TextMeshProUGUI Text;
    
        [SerializeField]
        private int _attributeKey;

        public int AttributeKey
        {
            get => _attributeKey;
            set => _attributeKey = value;
        }

        public void Display(int value)
        {
            Text.text = value.ToString();
        }
    }
}