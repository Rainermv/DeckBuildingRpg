using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.CardSystem;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Collection;
using Assets.Scripts.CardSystem.Model.Command;
using Assets.Scripts.CardSystem.View;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {

        public CardSystemViewController CardSystemViewController;

        private CardSystemController _cardSystemController;

        // Start is called before the first frame update
        void Start()
        {
            _cardSystemController = new CardSystemController();
            var cardSystemModel = _cardSystemController.Initialize(CardSystemModelFactory.Build());

            CardSystemViewController.Initialize(cardSystemModel, OnCardClicked, onDeckClicked);


        }

        private async void OnCardClicked(CardView cardView)
        {
            var player = cardView.Card.Collection.CardPlayer;
            
            switch (cardView.Card.Collection.CollectionIdentifier)
            {
                case CardCollectionIdentifier.Hand:
                    await _cardSystemController.MoveCardTo(cardView.Card, player.CardCollections[CardCollectionIdentifier.Discard]);
                    break;

            }
        }


        private async void onDeckClicked(CardCollectionView collectionView)
        {
            var player = collectionView.CardCollection.CardPlayer;
            
            switch (collectionView.CardCollection.CollectionIdentifier)
            {
                case CardCollectionIdentifier.Deck:
                    await _cardSystemController.DrawCards(player,
                        player.CardCollections[CardCollectionIdentifier.Deck],
                        player.CardCollections[CardCollectionIdentifier.Hand]);
                    break;
                    
            }

        }


        private Card MakeCard(string cardName, int cardType)
        {
            var card = Card.Make(cardName, cardType);

            card.Commands = new List<ICardCommand>()
            {
                new SwitchTypeCommand(card)
            };

            card.OnUpdate += () => OnCardUpdate(card);

            card.OnStartPlay += OnCardStartPlay;
            card.OnComandRun += OnCardCommandRun;
            card.OnFinishPlay += OnCardFinishPlay;

            card.CardType = cardType;

            return card;

        }

        private void OnCardUpdate(Card obj)
        {
        }

        private void OnCardFinishPlay(Card c, CardPlayReport carPlayReport)
        {
        }

        private void OnCardCommandRun(Card c, CardPlayReport arg2, CardCommandReport arg3)
        {
        }

        private void OnCardStartPlay(Card c, CardPlayReport arg2)
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}