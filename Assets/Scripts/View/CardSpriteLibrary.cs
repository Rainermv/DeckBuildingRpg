using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class CardSpriteLibrary 
    {
        private List<Sprite> _cardSprites;

        public CardSpriteLibrary(List<Sprite> cardSprites)
        {
            _cardSprites = cardSprites;
        }

        public Sprite Get(int imageIndex)
        {
            if (imageIndex < _cardSprites.Count)
            {
                return _cardSprites[imageIndex];
            }

            return _cardSprites.FirstOrDefault();
        }
    }
}