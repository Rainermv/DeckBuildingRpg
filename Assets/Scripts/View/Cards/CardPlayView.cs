using System;
using Assets.Scripts.Core.Model.Cards;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.View.Cards
{
    public class CardPlayView : SerializedMonoBehaviour, IPointerMoveHandler
    {
        [SerializeField] private CardView _cardView;

        public Action<PointerEventData, int> OnPointerUiEvent { get; set; }

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

        public void OnPointerMove(PointerEventData eventData)
        {
            OnPointerUiEvent(eventData, PointerEventTrigger.MOVE);
        }
    }
}