using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.Controller.CardShuffler;
using Assets.Scripts.Controller.MovementResolver;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Card.Collections;
using Assets.Scripts.Core.Model.Entity;
using Assets.Scripts.Core.Model.GridMap;
using Assets.Scripts.Core.Utility;

namespace Assets.Scripts.Controller
{
    public class BattleController
    {
        private readonly ICardShuffler _cardShuffler;
        private IPathFindResolver _pathFindResolver;

        private Entity _controlledEntity;

        private Dictionary<GridPosition, GridTile> _tileDictionary;
        private List<Entity> _battleEntities;
        private CombatModel _combatModel;

        private GridMapPathfindingModel _gridMapPathfindingModel;
        private CardPlay _CardPlay;

        public BattleController(ICardShuffler cardShuffler, IPathFindResolver pathFindResolver)
        {
            _gridMapPathfindingModel = new GridMapPathfindingModel();
            _cardShuffler = cardShuffler;
            _pathFindResolver = pathFindResolver;

            _pathFindResolver.OnIsPositionValid = 
                position => GridUtilities.IsPositionValid(position, _tileDictionary, _battleEntities);

            _pathFindResolver.OnGetCostToCrossAtoB =
                (position, gridPosition) => GridUtilities.GetCostToPosition(position, gridPosition, _tileDictionary, _battleEntities);
        }


        public CombatModel Setup(CombatModel combatModel)
        {
            _combatModel = combatModel;

            foreach (var player in combatModel.Players)
            {
                var deck = player.CardCollections[CardCollectionIdentifier.Deck];
                deck.Cards = _cardShuffler.Run(deck.Cards);
            }

            _tileDictionary = combatModel.GridMapModel.GridTiles.ToDictionary(tile => tile.GridPosition, tile => tile);
            _battleEntities = combatModel.Entities;

            _controlledEntity = combatModel.Entities[0];

            return combatModel;
        }

        public GridMapPathfindingModel OnGridFindPathToTarget(GridPosition targetGridPosition)
        {
            var initialGridPosition = _controlledEntity.GridPosition;

            if (_gridMapPathfindingModel.GridPositions.Any() &&
                _gridMapPathfindingModel.GridPositions.First() == initialGridPosition &&
                _gridMapPathfindingModel.GridPositions.Last() == targetGridPosition)
            {
                return _gridMapPathfindingModel;
            }

            var result = _pathFindResolver.FindPathToTarget(initialGridPosition, targetGridPosition);
            _gridMapPathfindingModel = new GridMapPathfindingModel()
            {
                MovementRange = _controlledEntity.MovementRange,
                GridPositions = result.MovementPathPositions,
            };
            return _gridMapPathfindingModel;

        }

        public async Task<MovePathResult> OnGridMovePath()
        {
            var pathSequence = _gridMapPathfindingModel.GridPositions.GetRange(0,
                Math.Min(_gridMapPathfindingModel.MovementRange + 1, _gridMapPathfindingModel.GridPositions.Count));

            await _controlledEntity.MovePathAsync(pathSequence, 250);

            _gridMapPathfindingModel.GridPositions.Clear();

            return new MovePathResult()
            {
                MovedEntity = _controlledEntity,
                PathSequence = pathSequence
            };
        }


        public CardPlay OnCardActivate(Card card)
        {
            var player = card.CardCollectionModelParent.PlayerParent;

            switch (card.CardCollectionModelParent.CollectionIdentifier)
            {
                case CardCollectionIdentifier.Hand:

                    //todo: decide if the card play is valid

                    _CardPlay = new CardPlay()
                    {
                        Player = player,
                        Card = card,
                        IsPlayValid = true
                    };
                    return _CardPlay;

            }

            return new CardPlay()
            {
                Player = player, 
                Card = card,
                IsPlayValid = false
            };
        }

     
    }

    public class MovePathResult
    {
        public Entity MovedEntity { get; set; }
        public List<GridPosition> PathSequence { get; set; }
    }

    public static class AttributeKey
    {
        public const int Power = 0;
        public const int Health = 1;
        public const int CardType = 2;
        public const int PowerCost = 3;
    }
}