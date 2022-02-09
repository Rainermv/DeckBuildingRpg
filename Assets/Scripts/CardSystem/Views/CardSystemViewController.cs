using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Collection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CardSystem.View
{
    public class CardSystemViewController : MonoBehaviour
    {
        public TextMeshProUGUI PowerText;
        public Button ChangePlayerButton;

        //public Dictionary<CardCollectionIdentifier, CardCollectionView> SceneCardCollectionViews;
        public CardCollectionView PlayerDeckCollectionView;
        public CardCollectionView PlayerHandCollectionView;
        public CardCollectionView PlayerDiscardCollectionView;
        
        public CardView CardPrefab;
        private Action<CardView> _onCardViewClicked;

        private CardPlayer _displayedPlayer;
        private LinkedList<CardPlayer> _linkedPlayerList;

        public void Initialize(CardSystemModel cardSystemModel,
            Action<CardView> onCardViewClicked,
            Action<CardCollectionView> onCardCollectionViewClicked)
        {
            _onCardViewClicked = onCardViewClicked;

            PlayerDeckCollectionView.Initialize(OnInstantiateCardViews, onCardCollectionViewClicked);
            PlayerHandCollectionView.Initialize(OnInstantiateCardViews, onCardCollectionViewClicked);
            PlayerDiscardCollectionView.Initialize(OnInstantiateCardViews, onCardCollectionViewClicked);

            _linkedPlayerList = new LinkedList<CardPlayer>(cardSystemModel.CardPlayers.Values);

            ChangePlayerButton.onClick.AddListener(() =>
            {
                DisplayPlayer(GetNextPlayer());
            });

        }

        

        public CardPlayer GetNextPlayer()
        {
            // Find the current node
            var curNode = _linkedPlayerList.Find(_displayedPlayer);

            // Point to the next
            LinkedListNode<CardPlayer> nextNode = curNode.Next;

            // Check if at the end of the list
            nextNode = nextNode == null ? _linkedPlayerList.First : nextNode;

            _displayedPlayer = nextNode.Value;
            return _displayedPlayer;
        }

        public void DisplayPlayer(CardPlayer cardPlayer)
        {
            _displayedPlayer = cardPlayer;

            foreach (var (cardCollectionIdentifier, cardCollection) in cardPlayer.CardCollections)
            {
                var cardCollectionView = CollectionViewFromIdentifier(cardCollectionIdentifier);
                if (cardCollectionView == null)
                {
                    Debug.LogError($"{cardCollectionIdentifier.ToString()} not found in SceneCardCollectionViews");
                    continue;
                }

                cardCollectionView.Display(cardCollection);

                //cardCollection.OnUpdate();
            }

            PowerText.text = cardPlayer.Resources[PlayerResourceNames.Power].Value.ToString();
            cardPlayer.Resources[PlayerResourceNames.Power].OnValueChanged = (res) => PowerText.text = res.Value.ToString();
        }


        private CardCollectionView CollectionViewFromIdentifier(CardCollectionIdentifier cardCollectionIdentifier)
        {
            switch (cardCollectionIdentifier)
            {
                case CardCollectionIdentifier.Deck:
                    return PlayerDeckCollectionView;

                case CardCollectionIdentifier.Hand:
                    return PlayerHandCollectionView;

                case CardCollectionIdentifier.Discard:
                    return PlayerDiscardCollectionView;

                default:
                    return null;
            }
        }


        private List<CardView> OnInstantiateCardViews(int numberOfCards, RectTransform parent)
        {
            var cardViews = new List<CardView>();
            for (int i = 0; i < numberOfCards; i++)

            {
                var cardView = Instantiate(CardPrefab);

                cardView.RectTransform.SetParent(parent, false);
                cardViews.Add(cardView);

                cardView.Initialize(_onCardViewClicked);
                // VIEW -> CONTROLLER
                //cardView.CardButton.onClick.AddListener(() => _onCardClicked(card, cardView));

                //card.OnUpdate();
            }

            return cardViews;

        }

    }
}