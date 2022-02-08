using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Collection;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CardSystem.View
{
    public class CardSystemViewController : MonoBehaviour
    {
        public Button ChangePlayerButton;

        //public Dictionary<CardCollectionIdentifier, CardCollectionView> SceneCardCollectionViews;
        public CardCollectionView PlayerDeckCollectionView;
        public CardCollectionView PlayerHandCollectionView;
        public CardCollectionView PlayerDiscardCollectionView;
        
        public CardView CardPrefab;
        private Action<CardView> _onCardViewClicked;


        private int _displayedPlayer;

        public void Initialize(CardSystemModel cardSystemModel,
            Action<CardView> onCardViewClicked,
            Action<CardCollectionView> onCardCollectionViewClicked)
        {
            _onCardViewClicked = onCardViewClicked;

            PlayerDeckCollectionView.Initialize(OnInstantiateCardViews, onCardCollectionViewClicked);
            PlayerHandCollectionView.Initialize(OnInstantiateCardViews, onCardCollectionViewClicked);
            PlayerDiscardCollectionView.Initialize(OnInstantiateCardViews, onCardCollectionViewClicked);

            currentPlayer = cardSystemModel.CardPlayers.Values.FirstOrDefault();

            DisplayPlayer(currentPlayer);

            linkedPlayerList = new LinkedList<CardPlayer>(cardSystemModel.CardPlayers.Values);

            ChangePlayerButton.onClick.AddListener(() =>
            {
                DisplayPlayer(GetNextPlayer());
            });

        }

        private CardPlayer currentPlayer;
        private LinkedList<CardPlayer> linkedPlayerList;

        public CardPlayer GetNextPlayer()
        {
            // Find the current node
            var curNode = linkedPlayerList.Find(currentPlayer);

            // Point to the next
            LinkedListNode<CardPlayer> nextNode = curNode.Next;

            // Check if at the end of the list
            nextNode = nextNode == null ? linkedPlayerList.First : nextNode;

            currentPlayer = nextNode.Value;
            return currentPlayer;
        }

        private void DisplayPlayer(CardPlayer cardPlayer)
        {
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
                cardView.GetComponent<RectTransform>().SetParent(parent, false);
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