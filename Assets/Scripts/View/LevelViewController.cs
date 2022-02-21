using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Model;
using Assets.Scripts.Model.GridMap;
using Assets.Scripts.View.Card;
using Assets.Scripts.View.GridMap;
using Assets.Scripts.View.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.View
{
    public class LevelViewController : SerializedMonoBehaviour
    {
        [SerializeField, AssetsOnly] private CharacterView _characterViewPrefab;

        [SerializeField, SceneObjectsOnly] private CardSystemView _cardView;
        [SerializeField, SceneObjectsOnly] private GridMapView _gridMapView;
        [SerializeField, SceneObjectsOnly] private Transform _charactersContainer;

        private Func<GridPosition, CharacterPathfindingModel> _onFindPathToTargetGrid;
        private Action _onExecuteMovement;

        private LevelViewModel _levelViewModel;
        
        public void Initialize(LevelModel levelModel,
            Action<CardView> onCardClicked,
            Action<CardCollectionView> onCardCollectionClicked,
            Func<GridPosition, CharacterPathfindingModel> onFindPathToTargetGrid,
            Action onExecuteMovement)
        {
            _levelViewModel = new LevelViewModel();
     
            _onExecuteMovement = onExecuteMovement;
            _onFindPathToTargetGrid = onFindPathToTargetGrid;
            
            _gridMapView.Initialize(levelModel.GridMapModel, OnTilemapPointerEvent);

            _cardView.Initialize(levelModel.Players,
                onCardClicked,
                onCardCollectionClicked);

            //todo: move characters code to a lower level
            foreach (var character in levelModel.Entities)
            {
                var characterView = Instantiate(_characterViewPrefab, _charactersContainer);

                characterView.Initialize(character, _gridMapView.CellToWorld);
            }

        }

        private void OnTilemapPointerEvent(PointerEventData pointerEventData, int pointerEventTrigger)
        {
            if (!pointerEventData.pointerCurrentRaycast.isValid)
            {
                return;
            }


            var gridPosition = _gridMapView.WorldToTilemapGrid(pointerEventData.pointerCurrentRaycast.worldPosition);

            switch (pointerEventTrigger)
            {
                case PointerEventTrigger.MOVE:
                    var pathfindModel = _onFindPathToTargetGrid(gridPosition);
                    _gridMapView.DrawHighlight(pathfindModel.GridPositions, pathfindModel.MoveRange);
                    break;

                case PointerEventTrigger.EXIT:
                    _gridMapView.ClearHighlight();
                    break;

                case PointerEventTrigger.DOWN:
                    _gridMapView.ClearHighlight();
                    _onExecuteMovement();
                    break;

            }
        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////
        /// </summary>

        private float minSize = 5;
        private float maxSize = 10;
        float sensitivity = 10f;
        private float speed = 10f;

        void Update()
        {
            var camOrthographicSize = CameraController.OrthographicSize - UnityEngine.Input.GetAxis("Mouse ScrollWheel") * sensitivity;

            CameraController.OrthographicSize = Mathf.Clamp(camOrthographicSize, minSize, maxSize);
            
            CameraController.MoveCamera(Vector3.up * speed * UnityEngine.Input.GetAxis("Vertical") * Time.deltaTime);;
            CameraController.MoveCamera(Vector3.right * speed * UnityEngine.Input.GetAxis("Horizontal") * Time.deltaTime);

        }

        
    }
}