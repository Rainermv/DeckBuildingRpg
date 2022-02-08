using System;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.CardCommand;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.CardSystem.View
{
    public class CardView : MonoBehaviour, IPointerClickHandler
    {
        public TextMeshProUGUI TextName;

        private Action<CardView> _onCardViewClicked;
        public Card Card { get; private set; }

        // Start is called before the first frame update
        public void Initialize(Action<CardView> onCardViewClicked)
        {
            _onCardViewClicked = onCardViewClicked;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _onCardViewClicked(this);
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

        public void SetCard(Card card)
        {
            Card = card;
            card.OnUpdate = OnCardUpdate;
        }

        public void OnCardUpdate()
        {
            gameObject.name = Card.Name;
            TextName.text = Card.Name;

            switch (Card.CardType)
            {
                case 1:
                    GetComponent<Image>().color = Color.blue;
                    break;

                case 2:
                    GetComponent<Image>().color = Color.red;
                    break;
            }
        }


        
    }
}
