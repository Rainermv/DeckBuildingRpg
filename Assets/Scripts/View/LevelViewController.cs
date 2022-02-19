using System;
using System.Collections.Generic;
using Assets.Scripts.Model;
using Assets.Scripts.Model.GridMap;
using Assets.Scripts.View.Card;
using Assets.Scripts.View.GridMap;
using Assets.Scripts.View.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class LevelViewController : SerializedMonoBehaviour
    {
        [SerializeField, AssetsOnly] private CharacterView _characterViewPrefab;

        [SerializeField, SceneObjectsOnly] private CardSystemView _cardView;
        [SerializeField, SceneObjectsOnly] private GridMapView _gridMapView;
        [SerializeField, SceneObjectsOnly] private Transform _charactersContainer;

        private Func<GridPosition, FindPathResult> _onFindPathToTargetGrid;

        private LevelViewModel _levelViewModel;
        
        public void Initialize(LevelModel levelModel,
            Action<CardView> onCardClicked,
            Action<CardCollectionView> onCardCollectionClicked,
            Func<GridPosition, FindPathResult> onFindPathToTargetGrid)
        {
            _levelViewModel = new LevelViewModel()
            {
                MovePredictionModel = new MovePredictionModel()
                {
                    MovementPathPositions = new List<GridPosition>(), 
                    TargetPosition = new GridPosition(-1000,-1000) //todo: target position to be last on the path
                }
            };


            _onFindPathToTargetGrid = onFindPathToTargetGrid;
            
            _gridMapView.Initialize(levelModel.GridMapModel, 
                OnTilemapPointerMove, 
                OnTilemapPointerDown);

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

        private void OnTilemapPointerDown(GridPosition gridPosition)
        {
            
            

        }

        private void OnTilemapPointerMove(GridPosition gridPosition)
        {
            if (_levelViewModel.MovePredictionModel.TargetPosition == gridPosition)
            {
                return;
            }

            var pathfindingResult = _onFindPathToTargetGrid(gridPosition);

            if (!pathfindingResult.PathFound)
            {
                _gridMapView.DrawPath(new List<GridPosition>());
                return;
            }

            _levelViewModel.MovePredictionModel.MovementPathPositions = pathfindingResult.MovementPathPositions;
            _gridMapView.DrawPath(pathfindingResult.MovementPathPositions);
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