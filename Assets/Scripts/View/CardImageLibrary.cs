using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.View
{

    [CreateAssetMenu(menuName  = "Card/Image Library", fileName = "ImageLibrary")]
    public class CardImageLibrary : ScriptableObject
    {
        [SerializeField]
        private Sprite[] _cardImageArray;

        public void Awake()
        {
        }


        public Sprite Get(int imageIndex)
        {
            if (imageIndex < _cardImageArray.Length)
            {
                return _cardImageArray[imageIndex];
            }

            return _cardImageArray[0];
        }
    }
}