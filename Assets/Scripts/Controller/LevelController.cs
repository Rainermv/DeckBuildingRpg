using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.CardSystem.Models.Attributes;
using Assets.Scripts.Controller.Factories;
using Assets.Scripts.Model;
using Assets.Scripts.Model.CardModel;
using Assets.Scripts.Model.CardModel.Collections;
using Assets.Scripts.Model.CardModel.Commands;
using Assets.Scripts.Model.CharacterModel;
using Assets.Scripts.Model.GridModel;
using Assets.Scripts.Systems.CardSystem.Constants;
using Assets.Scripts.Systems.CardSystem.Utility;
using Assets.Scripts.Systems.GridSystem;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Controller
{
    internal class LevelController
    {
        private readonly ICardShuffler _cardShuffler;

        private LevelModel _levelModel;
        private Character _controlledCharacter;
        private Dictionary<GridPosition, GridTile> _tileDictionary;

        public LevelController(ICardShuffler cardShuffler)
        {
            _cardShuffler = cardShuffler;

        }

        public LevelModel Setup(LevelModel levelModel)
        {
            _levelModel = levelModel;

            foreach (var player in levelModel.Players.Values)
            {
                var deck = player.CardCollections[CardCollectionIdentifier.Deck];
                deck.Cards = _cardShuffler.Run(deck.Cards);
            }

            _tileDictionary = _levelModel.GridMapModel.GridTiles.ToDictionary(tile => tile.GridPosition, tile => tile);

            _controlledCharacter = _levelModel.Characters[0];

            return levelModel;
        }

        public void OnGridPositionInput(GridPosition moveGridPosition)
        {
            if (!_tileDictionary.TryGetValue(moveGridPosition, out var tileAtPosition))
            {
                return;
            }

            if (tileAtPosition.PositionedEntity != null)
            {
                return;
            }



            var characterPreviousTile = _tileDictionary[_controlledCharacter.GridPosition];

            characterPreviousTile.PositionedEntity = null;
            
            _controlledCharacter.GridPosition = tileAtPosition.GridPosition;
            tileAtPosition.PositionedEntity = _controlledCharacter;
            

        }

        private static bool InsideBounds(GridMapModel gridMapModel, GridPosition gridPosition)
        {
            return new Rect(0, 0, gridMapModel.Width, gridMapModel.Height).Contains(new Vector2(gridPosition.X,
                gridPosition.Y));
        }


        public async void OnCardClicked(Card card)
        {
            var player = card.CardCollectionParent.PlayerParent;

            switch (card.CardCollectionParent.CollectionIdentifier)
            {
                case CardCollectionIdentifier.Hand:

                    //if (player.AttributeSet.GetValue(PlayerAttributeNames.Power))

                    card.Play(_levelModel);
                    CardService.MoveCardTo(card, player.CardCollections[CardCollectionIdentifier.Discard]);
                    break;

            }
        }

        public void OnCardCollectionClicked(CardCollection cardCollection)
        {
            if (cardCollection.CollectionIdentifier == CardCollectionIdentifier.Deck)
            {
                CardService.DrawCards(cardCollection, cardCollection.PlayerParent.CardCollections[CardCollectionIdentifier.Hand], 1);
            }
        }


        
    }

    public static class AttributeKey
    {
        public const int Power = 0;
        public const int Health = 1;
        public const int CardType = 2;
        public const int PowerCost = 3;
    }
}