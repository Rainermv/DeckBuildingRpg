using Assets.Scripts.Controller.CardShuffler;
using Assets.Scripts.Controller.MovementResolver;
using Assets.Scripts.Model;
using Assets.Scripts.Model.Actor;
using Assets.Scripts.Model.Card;
using Assets.Scripts.Model.Card.Collections;
using Assets.Scripts.Model.GridMap;
using Assets.Scripts.Utility;
using Assets.Scripts.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    internal class LevelController
    {
        private readonly ICardShuffler _cardShuffler;
        private IPathFindResolver _pathFindResolver;

        private Entity _controlledEntity;

        private Dictionary<GridPosition, GridTile> _tileDictionary;
        private List<Entity> _entities;
        private LevelModel _levelModel;

        private CharacterPathfindingModel _characterPathfindingModel;

        public LevelController(ICardShuffler cardShuffler, IPathFindResolver pathFindResolver)
        {
            _characterPathfindingModel = new CharacterPathfindingModel();
            _cardShuffler = cardShuffler;
            _pathFindResolver = pathFindResolver;

            _pathFindResolver.OnIsPositionValid = position => GridUtilities.IsPositionValid(position, _tileDictionary, _entities);
            _pathFindResolver.OnGetCostToCrossAtoB =
                (position, gridPosition) => GridUtilities.GetCostToPosition(position, gridPosition, _tileDictionary, _entities);
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

        public CharacterPathfindingModel OnFindPathToTarget(GridPosition targetGridPosition)
        {
            if (_characterPathfindingModel.GridPositions.Any() &&
                _characterPathfindingModel.GridPositions.Last() == targetGridPosition)
            {
                return _characterPathfindingModel;
            }

            var result = _pathFindResolver.FindPathToTarget(_controlledEntity.GridPosition, targetGridPosition);
            _characterPathfindingModel = new CharacterPathfindingModel()
            {
                MoveRange = 4,
                GridPositions = result.MovementPathPositions,
            };
            return _characterPathfindingModel;

        }

        public async void OnExecuteMovement()
        {
            
            foreach (var moveGridPosition in _characterPathfindingModel.GridPositions.GetRange(0, 
                Math.Min(_characterPathfindingModel.MoveRange+1, _characterPathfindingModel.GridPositions.Count)))
            {
                Debug.Log($"Moving to {moveGridPosition}");
                await _controlledEntity.SetPositionAsync(moveGridPosition);

                await Task.Delay(250);
            }

            _characterPathfindingModel.GridPositions.Clear();
        }


        public void OnCardClicked(CardModel cardModel)
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