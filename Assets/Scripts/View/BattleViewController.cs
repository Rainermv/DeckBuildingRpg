using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.Controller;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.GridMap;
using Assets.Scripts.View.Card;
using Assets.Scripts.View.GridMap;
using Assets.Scripts.View.ViewModel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.View
{
    public class BattleViewController : SerializedMonoBehaviour
    {
        [SerializeField, AssetsOnly] private CharacterView _characterViewPrefab;

        [SerializeField, SceneObjectsOnly] private CameraController _cameraController;
        [SerializeField, SceneObjectsOnly] private CardSystemView _cardSystemView;
        [SerializeField, SceneObjectsOnly] private GridMapView _gridMapView;
        [SerializeField, SceneObjectsOnly] private Transform _charactersContainer;
        [SerializeField, SceneObjectsOnly] private CardPlayView CardPlayView;
        
        private Func<GridPosition, GridMapPathfindingModel> _onFindPathToTargetGrid;
        private Func<Task<MovePathResult>> _onExecuteMovement;

        private BattleViewModel _battleViewModel;

        private List<CharacterView> _entityViews = new();

        public void Initialize(BattleModel battleModel,
            Func<CardModel, CardPlayModel> onCardClickedGetCardPlayModel,
            Func<GridPosition, GridMapPathfindingModel> onFindPathToTargetGrid,
            Func<Task<MovePathResult>> onExecuteMovement,
            CardSpriteLibrary cardSpriteLibrary)
        {
            _battleViewModel = new BattleViewModel();
     
            _onExecuteMovement = onExecuteMovement;
            _onFindPathToTargetGrid = onFindPathToTargetGrid;
            
            _gridMapView.Initialize(battleModel.GridMapModel, OnTilemapPointerEvent);
            _cardSystemView.Initialize(battleModel.Players, OnCardPointerEvent, cardSpriteLibrary);

            CardPlayView.Initialize(cardSpriteLibrary);

            //todo: move characters code to a lower level
            foreach (var entity in battleModel.Entities)
            {
                var characterView = Instantiate(_characterViewPrefab, _charactersContainer);

                entity.OnSetPosition +=
                    gridPosition => _cameraController.SmoothJumpTo(_gridMapView.GridToWorld(gridPosition));

                characterView.Initialize(entity, _gridMapView.GridToWorld);
                _entityViews.Add(characterView);
            }

            _cameraController.SmoothJumpTo(_entityViews[0].transform.position);

        }
        
        private async void OnTilemapPointerEvent(PointerEventData pointerEventData, int pointerEventTrigger)
        {
            if (!pointerEventData.pointerCurrentRaycast.isValid)
            {
                return;
            }

            var worldPosition = pointerEventData.pointerCurrentRaycast.worldPosition;

            var gridPosition = _gridMapView.WorldToTilemapGrid(worldPosition);

            switch (pointerEventTrigger)
            {
                case PointerEventTrigger.MOVE:
                    var pathfindModel = _onFindPathToTargetGrid(gridPosition);
                    _gridMapView.DrawHighlight(pathfindModel.GridPositions, pathfindModel.MovementRange);
                    break;

                case PointerEventTrigger.EXIT:
                    _gridMapView.ClearHighlight();
                    break;

                case PointerEventTrigger.DOWN: 
                    _gridMapView.ClearHighlight();
                    await _onExecuteMovement();
                    break;

            }
        }

        private void OnCardPointerEvent(CardModel cardModel, PointerEventData pointerEventData, int pointerTrigger)
        {
            switch (pointerTrigger)
            {
                case PointerEventTrigger.ENTER:
                    CardPlayView.Set(cardModel);
                    return;

                case PointerEventTrigger.UP:
                    CardPlayView.Hide();
                    return;

                case PointerEventTrigger.DOWN:
                    //var cardPlayModel = _onActivateCard(CardModel, poin);
                    return;
            }


            
        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////
        /// </summary>

        

        
    }
}