using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.CardSystem;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.CardCollection;
using Assets.Scripts.CardSystem.Model.CardCommand;
using Assets.Scripts.CardSystem.View;
using UnityEngine;

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

            _cardSystemController.AddCardCollection(DeckSystemConstants.COLLECTION_DECK, 
                CardCollection.Make(
                    new List<Card>()
                    {
                        MakeCard("Card 1", 1),
                        MakeCard("Card 2", 2)
                    }));

            _cardSystemController.AddCardCollection(DeckSystemConstants.COLLECTION_HAND,
                CardCollection.Make(
                    new List<Card>()
                    {
                        MakeCard("Card 3", 1)
                    }));

            var decks = _cardSystemController.Initialize();

            CardSystemViewController.Initialize(decks, OnCardClicked, onDeckClicked);

        }

        private void OnCardClicked(Card card, CardView cardView)
        {
            if (cardView.isActiveAndEnabled){
                card.Play();
            }
        }


        private async void onDeckClicked(CardCollection cardCollection, CardCollectionView cardCollectionView)
        {
            switch (cardCollectionView.Identifier)
            {
                case DeckSystemConstants.COLLECTION_DECK:
                    await _cardSystemController.DrawCards(DeckSystemConstants.COLLECTION_DECK, DeckSystemConstants.COLLECTION_HAND);
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