using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.CardSystem.Views
{
    public class StringCardAttributeView : MonoBehaviour, ICardAttributeView
    {
        public TextMeshProUGUI Text;
    
        [SerializeField]
        private string _attributeName;

        public string AttributeName
        {
            get => _attributeName;
            set => _attributeName = value;
        }

        public void Display(int value)
        {
            Text.text = value.ToString();
        }
    }

    public interface ICardAttributeView
    {
        string AttributeName { get; set; }
        void Display(int value);
    }
}