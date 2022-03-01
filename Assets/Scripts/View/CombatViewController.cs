using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.Controller;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.Core.Model.GridMap;
using Assets.Scripts.View.Cards;
using Assets.Scripts.View.GridMap;
using Assets.Scripts.View.ViewModel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.View
{
    public class CombatViewController : SerializedMonoBehaviour
    {
        [SerializeField, AssetsOnly, Required] private CharacterView _characterViewPrefab;

        [SerializeField, SceneObjectsOnly, Required] private CameraController _cameraController;
        [SerializeField, SceneObjectsOnly, Required] private CardSystemView _cardSystemView;
        [SerializeField, SceneObjectsOnly, Required] private GridMapView _gridMapView;
        [SerializeField, SceneObjectsOnly, Required] private Transform _charactersContainer;
        
        //private Func<GridPosition, GridMapPathfindingModel> _onFindPathToTargetGrid;
        //private Func<Task<MovePathResult>> _onExecuteMovement;

        private List<CharacterView> _entityViews = new();

        public void Initialize(CombatModel battleModel,
            Func<GridPosition, GridMapPathfindingModel> onFindPathToTargetGrid,
            Func<Task<MovePathResult>> onExecuteMovement,
            CardSpriteLibrary cardSpriteLibrary)
        {
            //_onExecuteMovement = onExecuteMovement;
            //_onFindPathToTargetGrid = onFindPathToTargetGrid;
            
            _gridMapView.Initialize(battleModel.GridMapModel, onFindPathToTargetGrid, onExecuteMovement);
            _cardSystemView.Initialize(battleModel.Players, cardSpriteLibrary);


            //todo: move characters code to a lower level
            foreach (var entity in battleModel.Entities)
            {
                var characterView = Instantiate(_characterViewPrefab, _charactersContainer);

                entity.OnEntitySetPosition +=
                    eventEntity => _cameraController.SmoothJumpTo(_gridMapView.GridToWorld(eventEntity.GridPosition));

                characterView.Initialize(entity, _gridMapView.GridToWorld);
                _entityViews.Add(characterView);
            }

            _cameraController.SmoothJumpTo(_entityViews[0].transform.position);

        }


        
       

        

        /// <summary>
        /// ///////////////////////////////////////////////////////////
        /// </summary>

        

        
    }
}