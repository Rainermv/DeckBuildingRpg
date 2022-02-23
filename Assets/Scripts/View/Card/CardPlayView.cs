using System;
using Assets.Scripts.Core.Model.Card;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.View.Card
{
    public class CardPlayView : SerializedMonoBehaviour
    {
        [SerializeField] private CardView _cardView;


        public void Initialize(CardSpriteLibrary cardSpriteLibrary)
        {
            _cardView.Initialize(cardSpriteLibrary);
            Hide();
        }

        public void Set(CardModel cardModel)
        {
            _cardView.gameObject.SetActive(true);
            _cardView.Display(cardModel);
        }

        public void Hide()
        {
            _cardView.gameObject.SetActive(false);
        }
    }
}