using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Controller;
using Assets.Scripts.Model.Card;
using Assets.Scripts.Model.Card.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.Card
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

        private Player _displayedPlayer;
        private LinkedList<Player> _linkedPlayerList;

        public void Initialize(Dictionary<string, Player> players,
            Action<CardView> onCardViewClicked,
            Action<CardCollectionView> onCardCollectionViewClicked)
        {
            _spriteLibraryList = SpriteLibrary.ToList();
            _onCardViewClicked = onCardViewClicked;

            PlayerDeckCollectionView.Initialize(OnInstantiateCardViews, onCardCollectionViewClicked);
            PlayerHandCollectionView.Initialize(OnInstantiateCardViews, onCardCollectionViewClicked);
            PlayerDiscardCollectionView.Initialize(OnInstantiateCardViews, onCardCollectionViewClicked);

            _linkedPlayerList = new LinkedList<Player>(players.Values);

            ChangePlayerButton.onClick.AddListener(() =>
            {
                DisplayPlayer(GetNextPlayer());
            });

            DisplayPlayer(players.Values.First());
        }

        

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
        }

        public void DisplayPlayer(Player player)
        {
            _displayedPlayer = player;

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

            player.AttributeSet.OnAttributeValueChange = (s, i) =>
            {
                Debug.Log($"{player.Name}: {s} is now {i}");
                switch (s)
                {
                    case AttributeKey.Power:
                        PlayerPowerText.text = $"{i}";
                        return;

                }
            };

            PlayerPowerText.text = $"{player.AttributeSet.GetValue(AttributeKey.Power)}";

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