using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
        private GameContext _gameContext;

        // Start is called before the first frame update
        void Start()
        {
            _gameContext = new GameContext()
            {
                CardSystemModel = CardSystemModelFactory.Build()
            };

            _cardSystemController = new CardSystemController(RulesetFactory.Build());

            _gameContext.CardSystemModel = _cardSystemController.Setup(_gameContext.CardSystemModel);

            CardSystemViewController.Initialize(_gameContext.CardSystemModel, OnCardClicked, onDeckClicked);
            CardSystemViewController.DisplayPlayer(_gameContext.CardSystemModel.CardPlayers.Values.First());
        }

        private async void OnCardClicked(CardView cardView)
        {
            var player = cardView.Card.CardCollectionParent.CardPlayerParent;
            
            switch (cardView.Card.CardCollectionParent.CollectionIdentifier)
            {
                case CardCollectionIdentifier.Hand:
                    cardView.Card.Play(_gameContext);
                    await _cardSystemController.MoveCardTo(cardView.Card, player.CardCollections[CardCollectionIdentifier.Discard]);
                    break;

            }
        }


        private async void onDeckClicked(CardCollectionView collectionView)
        {
            var player = collectionView.CardCollection.CardPlayerParent;
            
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
            var card = Card.Make(cardName);

            card.Commands = new List<ICardCommand>()
            {
                new SwitchTypeCommand(card)
            };

            card.OnUpdate += () => OnCardUpdate(card);

            card.OnStartPlay += OnCardStartPlay;
            card.OnCommandRun += OnCardCommandRun;
            card.OnFinishPlay += OnCardFinishPlay;

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