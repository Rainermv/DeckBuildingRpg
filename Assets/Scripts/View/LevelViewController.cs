using System;
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
        [SerializeField, SceneObjectsOnly] private ILevelInputListener _levelInputListener;

        private Action<GridPosition> _onGridInputTrigger;


        public void Initialize(LevelModel levelModel, Action<CardView> onCardClicked,
            Action<CardCollectionView> onCardCollectionClicked, Action<GridPosition> onGridInputTrigger)
        {
            _onGridInputTrigger = onGridInputTrigger;
            
            _levelInputListener.Initialize(OnWorldInputTrigger);

            _gridMapView.Initialize(levelModel.GridMapModel, () => _levelInputListener.WorldInputPosition);
            _cardView.Initialize(levelModel.Players,
                onCardClicked,
                onCardCollectionClicked);

            foreach (var character in levelModel.Entities)
            {
                var characterView = Instantiate(_characterViewPrefab, _charactersContainer);

                characterView.Initialize(character, _gridMapView.CellToWorld);
            }


            //CameraController..position += GridView.GridCenter;

        }

        void OnWorldInputTrigger(Vector3 worldPosition)
        {
            var cellPosition = _gridMapView.WorldToCell(new Vector3(worldPosition.x, worldPosition.y));

            if (!_gridMapView.PointWithinBounds(cellPosition))
                return;

            _onGridInputTrigger(new GridPosition(cellPosition.x, cellPosition.y));
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