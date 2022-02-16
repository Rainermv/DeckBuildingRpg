using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Ruleset;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Assets.Scripts.CardSystem.Views
{
    class ImageCardAttributeView : MonoBehaviour, ICardAttributeView
    {
        public Sprite[] _spriteLibrary;
        private List<Sprite> _spriteLibraryList;
        
        public Image _imageReference;

        [SerializeField]
        private AttributeKey _attributeKey;

        public AttributeKey AttributeKey
        {
            get => _attributeKey;
            set => _attributeKey = value;
        }

        
        public void Display(int value)
        {
            if (_spriteLibraryList == null)
            {
                _spriteLibraryList = _spriteLibrary.ToList();
            }

            if (value <= _spriteLibraryList.Count)
            {
                _imageReference.sprite = _spriteLibraryList[value];
            }
        }
    }
}