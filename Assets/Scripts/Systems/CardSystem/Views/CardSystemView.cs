using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.CardSystem.Constants;
using Assets.Scripts.CardSystem.Models;
using Assets.Scripts.CardSystem.Models.Collections;
using Assets.Scripts.Ruleset;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CardSystem.Views
{
    public class CardSystemView : MonoBehaviour
    {
        public Sprite[] SpriteLibrary;
        private List<Sprite> _spriteLibraryList;

        public TextMeshProUGUI PlayerPowerText;
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
            _spriteLibraryList = SpriteLibrary.ToList();
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
            }

            cardPlayer.AttributeSet.OnAttributeValueChange = (s, i) =>
            {
                Debug.Log($"{cardPlayer.Name}: {s} is now {i}");
                switch (s)
                {
                    case AttributeKey.Power:
                        PlayerPowerText.text = $"{i}";
                        return;

                }
            };

            PlayerPowerText.text = $"{cardPlayer.AttributeSet.GetValue(AttributeKey.Power)}";

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

                cardView.Initialize(_onCardViewClicked, index =>
                {
                    if (index <= _spriteLibraryList.Count)
                    {
                        return _spriteLibraryList[index];
                    }

                    return Sprite.Create(Texture2D.blackTexture, new Rect(this.GetComponent<RectTransform>().rect),Vector2.zero);

                });
                // VIEW -> CONTROLLER
                //cardView.CardButton.onClick.AddListener(() => _onCardClicked(card, cardView));

                //card.OnUpdate();
            }

            return cardViews;

        }

    }
}