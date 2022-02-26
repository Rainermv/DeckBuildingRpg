using System;
using Assets.Scripts.Controller;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.View.Attribute;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.View.Card
{
    public class CardView : SerializedMonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public RectTransform RectTransform;

        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private TextMeshProUGUI _textBlock;
        [SerializeField] private Image _cardImage;

        private Action<Core.Model.Card.Card, PointerEventData, int> _onCardPointerEvent;
        private CardSpriteLibrary _cardSpriteLibrary;

        public Core.Model.Card.Card Card { get; private set; }

        // Start is called before the first frame update
        public void Initialize(Action<Core.Model.Card.Card, PointerEventData, int> onCardPointerEvent, CardSpriteLibrary cardSpriteLibrary)
        {
            _onCardPointerEvent = onCardPointerEvent;
            _cardSpriteLibrary = cardSpriteLibrary;
        }

        public void Initialize(CardSpriteLibrary cardSpriteLibrary)
        {
            _cardSpriteLibrary = cardSpriteLibrary;
        }

        

        public void Reset(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void Display(Core.Model.Card.Card card)
        {
            Card = card;
            card.OnUpdate = OnCardUpdate;

            // Trigger update to display data
            OnCardUpdate();
        }

        public void OnCardUpdate()
        {
            gameObject.name = Card.Name;
            _textName.text = Card.Name;
            _textBlock.text = Card.Text;
            _cardImage.sprite = _cardSpriteLibrary.Get(Card.CardDataIndex);

            foreach (var attributeView in GetComponentsInChildren<ICardAttributeView>())
            {
                if (Card.AttributeSet.Contains(attributeView.AttributeKey))
                {
                    attributeView.Display(Card.AttributeSet.GetValue(attributeView.AttributeKey));
                }
                
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _onCardPointerEvent?.Invoke(Card, eventData, PointerEventTrigger.DOWN);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            _onCardPointerEvent?.Invoke(Card, eventData, PointerEventTrigger.ENTER);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _onCardPointerEvent?.Invoke(Card, eventData, PointerEventTrigger.EXIT);
        }
    }
}
