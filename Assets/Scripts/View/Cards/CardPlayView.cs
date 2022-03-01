using Assets.Scripts.Core.Model.Cards;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.View.Cards
{
    public class CardPlayView : SerializedMonoBehaviour
    {
        [SerializeField] private CardView _cardView;
        
        public void Initialize()
        {
            _cardView.ReportEvents = false;
            Hide();
        }

        public void Set(Card card, Sprite cardSprite)
        {
            _cardView.Display(card, cardSprite);
        }

        public void Hide()
        {
            _cardView.SetActive(false);
        }
    }
}