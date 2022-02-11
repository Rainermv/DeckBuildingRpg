using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Ruleset;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.CardSystem.Views
{
    public class StringCardAttributeView : MonoBehaviour, ICardAttributeView
    {
        public TextMeshProUGUI Text;
    
        [SerializeField]
        private AttributeKey _attributeKey;

        public AttributeKey AttributeKey
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