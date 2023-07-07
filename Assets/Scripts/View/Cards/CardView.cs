using System;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.View.Attribute;
using Sirenix.OdinInspector;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.View.Cards
{
    public class CardView : SerializedMonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler,
        IBeginDragHandler, ISelectHandler
    {
        //public Transform Transform;

        [SerializeField, ChildGameObjectsOnly, Required] private TextMeshPro _textName;
        [SerializeField, ChildGameObjectsOnly, Required] private TextMeshPro _textBlock;
        [SerializeField, ChildGameObjectsOnly, Required] private SpriteRenderer _imageRenderer;

        private Action<PointerEventData, int> _onPointerUIEvent;
        public bool ReportEvents { private get; set; }

  
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void Display(Card card, Sprite cardSprite)
        {
            gameObject.name = card.Name;
            _textName.text = card.Name;
            _textBlock.text = card.Text;
            _imageRenderer.sprite = cardSprite;

            foreach (var attributeView in GetComponentsInChildren<ICardAttributeView>())
            {
                if (card.Attributes.Contains(attributeView.AttributeKey))
                {
                    attributeView.Display(card.Attributes.GetValue(attributeView.AttributeKey));
                }
            }

            gameObject.SetActive(true);
            
            _onPointerUIEvent = (data, trigger) 
                => InteractionEvents.OnCardPointerUIEvent?.Invoke(this, card, data, trigger);

        }
        
        private void PointerEvent(PointerEventData eventData, int pointerEventTrigger)
        {
            if (!ReportEvents)
                return;

            _onPointerUIEvent?.Invoke(eventData, pointerEventTrigger);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PointerEvent(eventData, PointerEventTrigger.CLICK);

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEvent(eventData, PointerEventTrigger.ENTER);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerEvent(eventData, PointerEventTrigger.EXIT);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            PointerEvent(eventData, PointerEventTrigger.BEGIN_DRAG);
        }

        public void OnSelect(BaseEventData eventData)
        {
            
            //PointerEvent(eventData., PointerEventTrigger.SELECT);
        }

        public void MoveTo(Vector2 stateModelPointerPosition)
        {
            throw new NotImplementedException();
        }
    }
}
