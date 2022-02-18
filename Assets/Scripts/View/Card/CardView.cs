using System;
using Assets.Scripts.Model.Card;
using Assets.Scripts.View.Attribute;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.View.Card
{
    public class CardView : MonoBehaviour, IPointerClickHandler
    {
        public RectTransform RectTransform;

        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private TextMeshProUGUI _textBlock;
        [SerializeField] private Image _cardImage;

        private Action<CardView> _onCardViewClicked;
        private Func<int, Sprite> _onGetSpriteFromIndex;
        public CardModel CardModel { get; private set; }

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

        public void Display(CardModel cardModel)
        {
            CardModel = cardModel;
            cardModel.OnUpdate = OnCardUpdate;

            // Trigger update to display data
            OnCardUpdate();
        }

        public void OnCardUpdate()
        {
            gameObject.name = CardModel.Name;
            _textName.text = CardModel.Name;
            _textBlock.text = CardModel.TextBlock;
            _cardImage.sprite = _onGetSpriteFromIndex(CardModel.ImageIndex);

            foreach (var attributeView in GetComponentsInChildren<ICardAttributeView>())
            {
                if (CardModel.AttributeSet.Contains(attributeView.AttributeKey))
                {
                    attributeView.Display(CardModel.AttributeSet.GetValue(attributeView.AttributeKey));
                }
                
            }
        }


        
    }
}
