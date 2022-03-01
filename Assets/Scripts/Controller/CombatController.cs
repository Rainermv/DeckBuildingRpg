using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.Controller.CardShuffler;
using Assets.Scripts.Controller.MovementResolver;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.Core.Model.Cards.Collections;
using Assets.Scripts.Core.Model.EntityModel;
using Assets.Scripts.Core.Model.GridMap;
using Assets.Scripts.Core.Utility;
using Assets.Scripts.View.Cards;
using Assets.TestsEditor;

namespace Assets.Scripts.Controller
{
    public class CombatController
    {

        private readonly ICardShuffler _cardShuffler;
        private readonly IPathFindResolver _pathFindResolver;


        private readonly CardPlayController _cardPlayController;


        private Entity _controlledEntity;

        private Dictionary<GridPosition, GridTile> _tileDictionary = new();
        private List<Entity> _battleEntities;
        private CombatModel _combatModel;

        private GridMapPathfindingModel _gridMapPathfindingModel;
        private CardPlayData _cardPlayData;


        public CombatController(ICardShuffler cardShuffler, IPathFindResolver pathFindResolver,
            CardScriptParser cardScriptParser)
        {
            _gridMapPathfindingModel = new GridMapPathfindingModel();
            _cardShuffler = cardShuffler;
            _pathFindResolver = pathFindResolver;

            _cardPlayController = new CardPlayController(cardScriptParser, FindTargetsResolver.OnCardScriptFindTarget);

            _pathFindResolver.OnIsPositionValid = 
                position => GridUtilities.IsPositionValid(position, _tileDictionary, _battleEntities);

            _pathFindResolver.OnGetCostToCrossAtoB =
                (position, gridPosition) => GridUtilities.GetCostToPosition(position, gridPosition, _tileDictionary, _battleEntities);


            GameplayEvents.OnCardEvent += (card, cardEvent) =>
            {
                if (cardEvent == CardEventIdentifiers.Activate)
                    _cardPlayController.OnCardActivate(card, _controlledEntity, _combatModel);
            };
        }


        public CombatModel Setup(CombatModel combatModel)
        {
            _combatModel = combatModel;

            _cardPlayController.SetupCardData(combatModel.CardDataList);

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

        
        public GridMapPathfindingModel OnGridFindPathToPosition(GridPosition finalGridPosition)
        {
            var initialGridPosition = _controlledEntity.GridPosition;

            if (_gridMapPathfindingModel.GridPositions.Any() &&
                _gridMapPathfindingModel.GridPositions.First() == initialGridPosition &&
                _gridMapPathfindingModel.GridPositions.Last() == finalGridPosition)
            {
                return _gridMapPathfindingModel;
            }

            var result = _pathFindResolver.FindPathToTarget(initialGridPosition, finalGridPosition);
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
        
        public void OnCombatStart()
        {
            foreach (var battleEntity in _battleEntities)
            {
                battleEntity.Attributes.SetValue(0, 0);
                battleEntity.Attributes.SetValue(1, 0);
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