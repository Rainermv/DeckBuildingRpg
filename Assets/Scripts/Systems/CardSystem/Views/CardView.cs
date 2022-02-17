using System;
using Assets.Scripts.Model.CardModel;
using Assets.Scripts.Systems.CardSystem.Views.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Systems.CardSystem.Views
{
    public class CardView : MonoBehaviour, IPointerClickHandler
    {
        public RectTransform RectTransform;

        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private TextMeshProUGUI _textBlock;
        [SerializeField] private Image _cardImage;

        private Action<CardView> _onCardViewClicked;
        private Func<int, Sprite> _onGetSpriteFromIndex;
        public Card Card { get; private set; }

        void Awake()
        {
        }

        // Start is called before the first frame update
        public void Initialize(Action<CardView> onCardViewClicked, Func<int, Sprite> onGetSpriteFromIndex)
        {
            _onGetSpriteFromIndex = onGetSpriteFromIndex;
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
            _cardImage.sprite = _onGetSpriteFromIndex(Card.ImageIndex);

            foreach (var attributeView in GetComponentsInChildren<ICardAttributeView>())
            {
                if (Card.AttributeSet.Contains(attributeView.AttributeKey))
                {
                    attributeView.Display(Card.AttributeSet.GetValue(attributeView.AttributeKey));
                }
                
            }
        }


        
    }
}
