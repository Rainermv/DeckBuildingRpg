using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Card.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.View.Card
{
    public class CardSystemView : SerializedMonoBehaviour
    {
        [AssetsOnly] public CardView CardPrefab;
        [AssetsOnly] public Sprite[] SpriteLibrary;

        //public Dictionary<CardCollectionIdentifier, CardCollectionView> SceneCardCollectionViews;
        [SceneObjectsOnly] public CardCollectionView PlayerDeckCollectionView;
        [SceneObjectsOnly] public CardCollectionView PlayerHandCollectionView;
        [SceneObjectsOnly] public CardCollectionView PlayerDiscardCollectionView;

        private List<Sprite> _spriteLibraryList;
        private LinkedList<Player> _linkedPlayerList;

        private Action<CardModel, PointerEventData, int> _onCardPointerEvent;
        private CardImageLibrary _cardImageLibrary;

        public void Initialize(Dictionary<string, Player> players,
            Action<CardModel, PointerEventData, int> onCardPointerEvent, CardImageLibrary cardImageLibrary)
        {
            _cardImageLibrary = cardImageLibrary;

            _onCardPointerEvent = onCardPointerEvent;
            _spriteLibraryList = SpriteLibrary.ToList();

            PlayerDeckCollectionView.Initialize(OnInstantiateCardViews);
            PlayerHandCollectionView.Initialize(OnInstantiateCardViews);
            PlayerDiscardCollectionView.Initialize(OnInstantiateCardViews);

            _linkedPlayerList = new LinkedList<Player>(players.Values);

            


            DisplayPlayer(players.Values.First());
        }

        

        /*
        public Player GetNextPlayer()
        {
            // Find the current node
            var curNode = _linkedPlayerList.Find(_displayedPlayer);

            // Point to the next
            LinkedListNode<Player> nextNode = curNode.Next;

            // Check if at the end of the list
            nextNode = nextNode == null ? _linkedPlayerList.First : nextNode;

            _displayedPlayer = nextNode.Value;
            return _displayedPlayer;
        }*/

        public void DisplayPlayer(Player player)
        {

            foreach (var (cardCollectionIdentifier, cardCollection) in player.CardCollections)
            {
                var cardCollectionView = CollectionViewFromIdentifier(cardCollectionIdentifier);
                if (cardCollectionView == null)
                {
                    Debug.LogError($"{cardCollectionIdentifier.ToString()} not found in SceneCardCollectionViews");
                    continue;
                }

                cardCollectionView.Display(cardCollection);
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

                cardView.RectTransform.SetParent(parent, false);
                cardViews.Add(cardView);

                cardView.Initialize(_onCardPointerEvent, _cardImageLibrary);
                // VIEW -> CONTROLLER
                //cardView.CardButton.onClick.AddListener(() => _onCardClicked(card, cardView));

                //card.OnUpdate();
            }

            return cardViews;

        }

        private Sprite OnGetSpriteFromIndex(int index)
        {
            if (index <= _spriteLibraryList.Count)
            {
                return _spriteLibraryList[index];
            }

            return Sprite.Create(Texture2D.blackTexture, new Rect(this.GetComponent<RectTransform>().rect), Vector2.zero);
        }
    }
}