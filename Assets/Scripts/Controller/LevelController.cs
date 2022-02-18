using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.Controller.CardShuffler;
using Assets.Scripts.Controller.MovementResolver;
using Assets.Scripts.Model;
using Assets.Scripts.Model.Actor;
using Assets.Scripts.Model.Card;
using Assets.Scripts.Model.Card.Collections;
using Assets.Scripts.Model.GridMap;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    internal class LevelController
    {
        private readonly ICardShuffler _cardShuffler;
        private IGridmovementResolver _movementResolver;


        private Entity _controlledEntity;

        private Dictionary<GridPosition, GridTile> _tileDictionary;
        private List<Entity> _entities;
        private LevelModel _levelModel;
        private bool canIssueCommands = true;

        public LevelController(ICardShuffler cardShuffler, IGridmovementResolver movementResolver)
        {
            _cardShuffler = cardShuffler;
            _movementResolver = movementResolver;
        }

        public LevelModel Setup(LevelModel levelModel)
        {
            _levelModel = levelModel;

            foreach (var player in levelModel.Players.Values)
            {
                var deck = player.CardCollections[CardCollectionIdentifier.Deck];
                deck.Cards = _cardShuffler.Run(deck.Cards);
            }

            _tileDictionary = levelModel.GridMapModel.GridTiles.ToDictionary(tile => tile.GridPosition, tile => tile);
            //_entityDictionary = levelModel.Characters.ToDictionary(actor => actor.GridPosition, actor => actor);
            _entities = levelModel.Entities;

            _controlledEntity = levelModel.Entities[0];

            return levelModel;
        }

        public async void OnGridPositionInput(GridPosition targetGridPosition)
        {
            if (!canIssueCommands)
            {
                Debug.LogWarning("Can't issue commands right now");
                return;
            }

            // If there is NOT a valid tile at the position, return
            if (!_tileDictionary.TryGetValue(targetGridPosition, out var targetGridTile))
            {
                return;
            }

            // if there is already an entity at the position, return
            if (_entities.Any(entity => entity.GridPosition == targetGridPosition))
            {
                return;
            }

            Debug.Log($"===============Starting position: {_controlledEntity.GridPosition}");
            var moveSequence = _movementResolver.MakeMoveSequence(_controlledEntity, targetGridPosition);
            Debug.Log(string.Join(" ", moveSequence.Select(position => position.ToString())));

            canIssueCommands = false;
            foreach (var moveGridPosition in moveSequence)
            {
                Debug.Log($"Moving to {moveGridPosition}");
                await _controlledEntity.SetPositionAsync(moveGridPosition);

                await Task.Delay(250);

            }
            canIssueCommands = true;
            Debug.Log($"============Final position: {_controlledEntity.GridPosition}");

        }

        private static bool InsideBounds(GridMapModel gridMapModel, GridPosition gridPosition)
        {
            return new Rect(0, 0, gridMapModel.Width, gridMapModel.Height).Contains(new Vector2(gridPosition.X,
                gridPosition.Y));
        }


        public async void OnCardClicked(CardModel cardModel)
        {
            var player = cardModel.CardCollectionModelParent.PlayerParent;

            switch (cardModel.CardCollectionModelParent.CollectionIdentifier)
            {
                case CardCollectionIdentifier.Hand:

                    //if (player.AttributeSet.GetValue(PlayerAttributeNames.Power))

                    cardModel.Play(_levelModel);
                    CardUtilities.MoveCardTo(cardModel, player.CardCollections[CardCollectionIdentifier.Discard]);
                    break;

            }
        }

        public void OnCardCollectionClicked(CardCollectionModel cardCollectionModel)
        {
            if (cardCollectionModel.CollectionIdentifier == CardCollectionIdentifier.Deck)
            {
                CardUtilities.DrawCards(cardCollectionModel, cardCollectionModel.PlayerParent.CardCollections[CardCollectionIdentifier.Hand], 1);
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