using System;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Command;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.CardSystem.View
{
    public class CardView : MonoBehaviour, IPointerClickHandler
    {
        public RectTransform RectTransform;
        [SerializeField] private TextMeshProUGUI _textName;

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

        public void OnStartPlay(Card card, CardPlayReport cardPlayReport)
        {
        }

        public void OnCommandRun(Card card, CardPlayReport cardPlayReport, CardCommandReport cardCommandReport)
        {
        }

        public void OnFinishPlay(Card card, CardPlayReport cardPlayReport)
        {
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

            switch (Card.Resources[CardResourceNames.POWER_EFFECT_TYPE].Value)
            {
                case 0:
                    GetComponent<Image>().color = Color.gray;
                    break;

                case 1:
                    GetComponent<Image>().color = Color.cyan;
                    break;

                case 2:
                    GetComponent<Image>().color = Color.magenta;
                    break;
            }
        }


        
    }
}
