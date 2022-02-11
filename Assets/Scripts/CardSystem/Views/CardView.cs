using System;
using Assets.Scripts.CardSystem.Constants;
using Assets.Scripts.CardSystem.Models;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.CardSystem.Views
{
    public class CardView : MonoBehaviour, IPointerClickHandler
    {
        public RectTransform RectTransform;

        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private TextMeshProUGUI _textBlock;

        private Action<CardView> _onCardViewClicked;
        public Card Card { get; private set; }

        void Awake()
        {
        }

        // Start is called before the first frame update
        public void Initialize(Action<CardView> onCardViewClicked)
        {
            _onCardViewClicked = onCardViewClicked;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _onCardViewClicked(this);
        }

        public void Reset(bool isActive)
        {
            gameObject.SetActive(isActive);

        }

        public void Display(Card card)
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
            _textBlock.text = Card.TextBlock;

            foreach (var attributeView in GetComponentsInChildren<ICardAttributeView>())
            {
                if (Card.AttributeSet.Contains(attributeView.AttributeName))
                {
                    attributeView.Display(Card.AttributeSet.GetValue(attributeView.AttributeName));
                }
                
            }

            switch (Card.AttributeSet.GetValue(CardAttributeNames.TYPE))
            {
                case CardTypes.DRAW:
                    //GetComponent<Image>().color = Color.gray;
                    break;

                case CardTypes.POWER:
                    //GetComponent<Image>().color = Color.cyan;
                    break;

                case CardTypes.ATTACK:
                    //GetComponent<Image>().color = Color.magenta;
                    break;
            }
        }


        
    }
}
